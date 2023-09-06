using Microsoft.EntityFrameworkCore;
using RapidPay.Repositories.CreateCards;
using RapidPay.Repositories.Payments;
using RapidPay.Repositories.UserRoles;
using RapidPay.Repositories.Users;

namespace RapidPay.Repositories.Data
{
    public class RapidPayDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RapidPayDbContext"/> class.
        /// <see cref="RapidPayDbContext"/> Constructor.
        /// </summary>
        /// <param name="options">Dependency injection options to set up the possibles configurations.</param>
        public RapidPayDbContext(DbContextOptions<RapidPayDbContext> options)
            : base(options)
        { }

        public DbSet<UserRole> UserRoles { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<CreditCard> CreditCards { get; set; }

        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new CreditCardConfiguration());
            modelBuilder.ApplyConfiguration(new PaymentConfiguration());
        }
    }
}
