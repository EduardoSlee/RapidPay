using Microsoft.EntityFrameworkCore;
using RapidPay.Repositories.Data;

namespace RapidPay.Repositories.CreateCards
{
    public class CreditCardsRepository : ICreditCardsRepository
    {
        private readonly RapidPayDbContext _rapidPayDbContext;

        public CreditCardsRepository(RapidPayDbContext rapidPayDbContext)
        {
            _rapidPayDbContext = rapidPayDbContext;
        }

        public async Task AddCreditCardAsync(CreditCard creditCard)
        {
            _rapidPayDbContext.CreditCards.Add(creditCard);
            await _rapidPayDbContext.SaveChangesAsync();
        }

        public async Task<CreditCard?> GetCreditCardAsync(string number)
        {
            return await _rapidPayDbContext.CreditCards
                .FirstOrDefaultAsync(creditCard => creditCard.Number == number);
        }

        public async Task UpdateCreditCardAsync(CreditCard creditCard)
        {
            _rapidPayDbContext.Update(creditCard);
            await _rapidPayDbContext.SaveChangesAsync();
        }

        public async Task<bool> CreditCardNumberExistsAsync(string number)
        {
            return await _rapidPayDbContext.CreditCards
                .Where(creditCard => creditCard.Number == number).AnyAsync();
        }
    }
}
