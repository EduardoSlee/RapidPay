using RapidPay.Repositories.Payments;
using RapidPay.Services.Payments.Models;

namespace RapidPay.Services.Payments
{
    public interface IPaymentsService
    {
        Task<PaymentResult> AddPaymentAsync(PaymentInput paymentInput);
    }
}
