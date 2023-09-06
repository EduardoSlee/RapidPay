namespace RapidPay.Services.CreditCards.Models
{
    public class CreditCardResult
    {
        public int Id { get; set; }

        public string Number { get; set; } = string.Empty;

        public decimal CreditLimit { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
