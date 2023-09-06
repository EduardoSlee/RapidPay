using RapidPay.Services.CreditCards.Models;

namespace RapidPay.Services.CreditCards
{
    public interface ICreditCardsService
    {
        Task<CreditCardResult> AddCreditCardAsync(CreditCardInput creditCardInput);

        Task<CardBalanceResult> GetCardBalanceAsync(CardBalanceInput cardBalanceInput);
    }
}
