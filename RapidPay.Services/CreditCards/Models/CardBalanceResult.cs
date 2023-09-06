namespace RapidPay.Services.CreditCards.Models
{
    public class CardBalanceResult
    {
        public string Number { get; set; } = string.Empty;

        public decimal CreditLimit { get; set; }

        public decimal AvailableCredit { get; set; }

        public decimal TotalPayments { get; set; }
    }
}
