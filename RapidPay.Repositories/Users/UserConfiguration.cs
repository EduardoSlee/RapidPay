using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RapidPay.Repositories.Users
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity)
        {
            entity.Property(e => e.UserName).HasDefaultValue(string.Empty);            
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.HasIndex(e => e.UserName).IsUnique(true);
        }
    }
}
