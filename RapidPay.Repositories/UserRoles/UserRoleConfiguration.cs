using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RapidPay.Repositories.UserRoles
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> entity)
        {
            entity.Property(e => e.Name).HasDefaultValue(string.Empty);            
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.HasIndex(e => e.Name).IsUnique(true);
        }
    }
}
