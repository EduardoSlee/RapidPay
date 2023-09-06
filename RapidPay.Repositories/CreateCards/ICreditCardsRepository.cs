namespace RapidPay.Repositories.CreateCards
{
    public interface ICreditCardsRepository
    {
        Task AddCreditCardAsync(CreditCard creditCard);

        Task<CreditCard?> GetCreditCardAsync(string number);

        Task UpdateCreditCardAsync(CreditCard creditCard);

        Task<bool> CreditCardNumberExistsAsync(string number);
    }
}
