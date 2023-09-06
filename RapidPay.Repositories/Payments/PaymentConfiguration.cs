using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RapidPay.Repositories.Payments
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> entity)
        {
            entity.Property(e => e.Amount).HasDefaultValue(0);
            entity.Property(e => e.FeeAmount).HasDefaultValue(0);
            entity.Property(e => e.TotalAmount).HasDefaultValue(0);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
        }
    }
}
