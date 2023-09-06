using FluentValidation;
using RapidPay.Repositories.CreateCards;
using RapidPay.Services.CreditCards.Models;

namespace RapidPay.Services.CreditCards.Validations
{
    public class CreditCardInputValidator : AbstractValidator<CreditCardInput>
    {
        private readonly ICreditCardsRepository _creditCardsRepository;

        public CreditCardInputValidator(ICreditCardsRepository creditCardsRepository)
        {
            _creditCardsRepository = creditCardsRepository;

            RuleFor(creditCardInput => creditCardInput.Number)
                .Must(CreditCardNumberIsNew)
                .WithMessage("There is another card with the same number.");
        }

        private bool CreditCardNumberIsNew(string creditNumber)
        {
            return !_creditCardsRepository.CreditCardNumberExistsAsync(creditNumber).GetAwaiter().GetResult();
        }
    }
}
