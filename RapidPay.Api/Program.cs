using RapidPay.Api.Configuration;
using RapidPay.EncryptionLibrary;
using RapidPay.Repositories.Data;
using RapidPay.Repositories.UserRoles;
using RapidPay.Repositories.Users;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .ConfigureApiServices(builder.Configuration)
    .AddBusinessServices()
    .AddDataServices(builder.Configuration)
    .AddPresentationServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Ensure database is created and populate with default data if needed
using (var scope = app.Services.CreateScope())
{
    var rapidPayDbContext = scope.ServiceProvider.GetRequiredService<RapidPayDbContext>();
    rapidPayDbContext.Database.EnsureCreated();

    if (!rapidPayDbContext.Users.Any())
    {
        // Create roles and users
        var adminRole = new UserRole
        {
            Id = 1,
            Name = "Admin"
        };
        var userRole = new UserRole
        {
            Id = 2,
            Name = "User"
        };

        // Save roles to generate IDs
        rapidPayDbContext.UserRoles.AddRange(adminRole, userRole);
        rapidPayDbContext.SaveChanges();

        // Encrypt passwords using AES encryption
        var encryptionKey = builder.Configuration.GetValue<string>("AppSettings:EncryptionKey");
        var encryptor = new AesEncryptor();

        // Create admin and regular user accounts
        var adminUser = new User
        {
            Id = 1,
            UserName = "admin",
            Password = Convert.ToBase64String(encryptor.EncryptAES("adminpassword", encryptionKey)),
            UserRoleId = adminRole.Id
        };

        var regularUser = new User
        {
            Id = 2,
            UserName = "user",
            Password = Convert.ToBase64String(encryptor.EncryptAES("userpassword", encryptionKey)),
            UserRoleId = userRole.Id
        };

        // Save users
        rapidPayDbContext.Users.AddRange(adminUser, regularUser);
        rapidPayDbContext.SaveChanges();
    }
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
