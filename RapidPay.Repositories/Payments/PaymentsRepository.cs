using Microsoft.EntityFrameworkCore;
using RapidPay.Repositories.Data;

namespace RapidPay.Repositories.Payments
{
    public class PaymentsRepository : IPaymentsRepository
    {
        private readonly RapidPayDbContext _rapidPayDbContext;

        public PaymentsRepository(RapidPayDbContext rapidPayDbContext)
        {
            _rapidPayDbContext = rapidPayDbContext;
        }

        public async Task AddPaymentAsync(Payment payment)
        {
            _rapidPayDbContext.Payments.Add(payment);
            await _rapidPayDbContext.SaveChangesAsync();

            await _rapidPayDbContext.Entry(payment)
                .Reference(payment => payment.CreditCard).LoadAsync();
        }

        public async Task<Payment?> GetLastestPaymentAsync()
        {
            return await _rapidPayDbContext.Payments
                .OrderByDescending(payment => payment.CreatedAt)
                .FirstOrDefaultAsync();
        }
    }
}
