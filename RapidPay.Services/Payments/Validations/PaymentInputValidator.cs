using FluentValidation;
using RapidPay.Repositories.CreateCards;
using RapidPay.Services.Payments.Models;

namespace RapidPay.Services.Payments.Validations
{
    public class PaymentInputValidator : AbstractValidator<PaymentInput>
    {
        private readonly ICreditCardsRepository _creditCardsRepository;

        public PaymentInputValidator(ICreditCardsRepository creditCardsRepository)
        {
            _creditCardsRepository = creditCardsRepository;

            RuleFor(paymentInput => paymentInput.CreditCardNumber)
                .Must(CreditCardNumberExists)
                .WithMessage("A credit card with specified number does not exist.");

            RuleFor(paymentInput => paymentInput.Amount)
                .Must(amount => amount > 0)
                .WithMessage("Amount cannot be less than or equal to 0.");
        }

        private bool CreditCardNumberExists(string creditNumber)
        {
            return _creditCardsRepository.CreditCardNumberExistsAsync(creditNumber).GetAwaiter().GetResult();
        }
    }
}
