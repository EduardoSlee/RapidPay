using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RapidPay.EncryptionLibrary;
using RapidPay.Repositories.CreateCards;
using RapidPay.Repositories.Data;
using RapidPay.Repositories.Payments;
using RapidPay.Repositories.Users;
using RapidPay.Services.CreditCards;
using RapidPay.Services.CreditCards.Validations;
using RapidPay.Services.Payments;
using RapidPay.Services.UFE;
using RapidPay.Services.Users;
using RapidPay.Services.Users.Mappings;
using System.Reflection;
using System.Text;

namespace RapidPay.Api.Configuration
{
    /// <summary>
    /// Static class used to add layers of services to the ASP.NET core service collection.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add configuration api services.
        /// </summary>
        /// <param name="services">ASP.NET core service collection.</param>
        /// /// <param name="configuration">Configuration settings.</param>
        /// <returns>ASP.NET core service collection.</returns>
        public static IServiceCollection ConfigureApiServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<AesEncryptor>();
            
            // Configure JWT authentication
            var jwtSettings = configuration.GetSection("JwtSettings");
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings["Issuer"],
                        ValidAudience = jwtSettings["Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"])),
                        ClockSkew = TimeSpan.Zero
                    };
                });

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(config =>
            {
                config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Scheme = "bearer",
                    Description = "Please insert the JWT Bearer in the field below."
                });
                config.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                  Array.Empty<string>()
                }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                config.IncludeXmlComments(xmlPath);
            });

            return services;
        }

        /// <summary>
        /// Add business layer services.
        /// </summary>
        /// <param name="services">ASP.NET core service collection.</param>
        /// <returns>ASP.NET core service collection.</returns>
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            // Register services
            return services
                .AddSingleton<UFEService>()
                .AddAutoMapper(typeof(UsersMappingProfile))
                .AddValidatorsFromAssemblyContaining<CreditCardInputValidator>()
                .AddScoped<ICreditCardsService, CreditCardsService>()
                .AddScoped<IPaymentsService, PaymentsService>()
                .AddScoped<IUsersService, UsersService>();
        }

        /// <summary>
        /// Add data access services and initialize Db context.
        /// </summary>
        /// <param name="services">ASP.NET core service collection.</param>
        /// <param name="configuration">Configuration settings.</param>
        /// <returns>ASP.NET core service collection.</returns>
        public static IServiceCollection AddDataServices(this IServiceCollection services, IConfiguration configuration)
        {
            var rapidPayConnectionString = configuration.GetConnectionString("RapidPayDb");

            services.AddDbContext<RapidPayDbContext>(
                options => options.UseSqlServer(rapidPayConnectionString));

            // Data access services
            return services
                .AddScoped<ICreditCardsRepository, CreditCardsRepository>()
                .AddScoped<IPaymentsRepository, PaymentsRepository>()
                .AddScoped<IUsersRepository, UsersRepository>();
        }

        /// <summary>
        /// Add presentation services to the ASP.NET core service collection.
        /// </summary>
        /// <param name="services">ASP.NET core service collection.</param>
        /// <returns>ASP.NET core service collection.</returns>
        public static IServiceCollection AddPresentationServices(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();

            return services;
        }
    }
}
