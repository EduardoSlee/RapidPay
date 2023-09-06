using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RapidPay.Repositories.CreateCards
{
    public class CreditCardConfiguration : IEntityTypeConfiguration<CreditCard>
    {
        public void Configure(EntityTypeBuilder<CreditCard> entity)
        {
            entity.Property(e => e.CreditLimit).HasDefaultValue(0);
            entity.Property(e => e.AvailableCredit).HasDefaultValue(0);
            entity.Property(e => e.TotalPayments).HasDefaultValue(0);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
            entity.HasIndex(e => e.Number).IsUnique(true);
        }
    }
}
