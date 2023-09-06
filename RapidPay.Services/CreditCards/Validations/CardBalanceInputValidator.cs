using FluentValidation;
using RapidPay.Repositories.CreateCards;
using RapidPay.Services.CreditCards.Models;

namespace RapidPay.Services.CreditCards.Validations
{
    public class CardBalanceInputValidator : AbstractValidator<CardBalanceInput>
    {
        private readonly ICreditCardsRepository _creditCardsRepository;

        public CardBalanceInputValidator(ICreditCardsRepository creditCardsRepository)
        {
            _creditCardsRepository = creditCardsRepository;

            RuleFor(creditCardInput => creditCardInput.Number)
                .Must(CreditCardNumberExists)
                .WithMessage("A credit card with specified number does not exist.");
        }

        private bool CreditCardNumberExists(string creditNumber)
        {
            return _creditCardsRepository.CreditCardNumberExistsAsync(creditNumber).GetAwaiter().GetResult();
        }
    }
}
