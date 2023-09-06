namespace RapidPay.Services.Payments.Models
{
    public class PaymentResult
    {
        public int Id { get; set; }

        public int CreditCardId { get; set; }

        public string CreditCardNumber { get; set; } = string.Empty;

        public decimal Amount { get; set; }

        public decimal FeeAmount { get; set; }

        public decimal TotalAmount { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
