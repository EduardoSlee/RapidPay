using AutoMapper;
using RapidPay.Repositories.CreateCards;
using RapidPay.Services.CreditCards.Models;

namespace RapidPay.Services.CreditCards.Mappings
{
    public class CreditCardsMappingProfile : Profile
    {
        public CreditCardsMappingProfile()
        {
            CreateMap<CreditCardInput, CreditCard>();

            CreateMap<CreditCard, CreditCardResult>();

            CreateMap<CreditCard, CardBalanceResult>();
        }
    }
}
