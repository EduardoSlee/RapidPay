using AutoMapper;
using RapidPay.Repositories.Payments;
using RapidPay.Services.Payments.Models;

namespace RapidPay.Services.Payments.Mappings
{
    public class PaymentsMappingProfile : Profile
    {
        public PaymentsMappingProfile()
        {
            CreateMap<PaymentInput, Payment>();

            CreateMap<Payment, PaymentResult>()
                .ForMember(paymentResult => paymentResult.CreditCardNumber, options =>
                    options.MapFrom(payment => payment.CreditCard.Number));
        }
    }
}
