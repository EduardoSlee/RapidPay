using AutoMapper;
using RapidPay.Repositories.CreateCards;
using RapidPay.Repositories.Payments;
using RapidPay.Services.Payments.Models;
using RapidPay.Services.UFE;
using System.Transactions;

namespace RapidPay.Services.Payments
{
    public class PaymentsService : IPaymentsService
    {
        private readonly IPaymentsRepository _paymentsRepository;

        private readonly ICreditCardsRepository _creditCardsRepository;

        private readonly UFEService _UFEService;

        private readonly IMapper _mapper;

        public PaymentsService(IPaymentsRepository paymentsRepository,
            ICreditCardsRepository creditCardsRepository,
            UFEService UFEService,
            IMapper mapper)
        {
            _paymentsRepository = paymentsRepository;
            _creditCardsRepository = creditCardsRepository;
            _UFEService = UFEService;
            _mapper = mapper;
        }

        public async Task<PaymentResult> AddPaymentAsync(PaymentInput paymentInput)
        {
            var payment = _mapper.Map<Payment>(paymentInput);

            payment.FeeAmount = Math.Round(payment.Amount * _UFEService.Fee, 2);
            payment.TotalAmount = payment.Amount + payment.FeeAmount;

            var creditCard = await _creditCardsRepository.GetCreditCardAsync(paymentInput.CreditCardNumber);
            
            if (creditCard.AvailableCredit - payment.TotalAmount < 0)
            {
                throw new InvalidOperationException("There is not enough available credit to perform the operation.");
            }

            payment.CreditCardId = creditCard.Id;

            creditCard.AvailableCredit -= payment.TotalAmount;
            creditCard.TotalPayments += payment.TotalAmount;

            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            await _paymentsRepository.AddPaymentAsync(payment);

            await _creditCardsRepository.UpdateCreditCardAsync(creditCard);

            scope.Complete();

            return _mapper.Map<PaymentResult>(payment);
        }
    }
}
