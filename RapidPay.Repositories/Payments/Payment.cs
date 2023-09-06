using RapidPay.Repositories.CreateCards;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RapidPay.Repositories.Payments
{
    [Table("Payments")]
    public class Payment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int CreditCardId { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public decimal FeeAmount { get; set; }

        [Required]
        public decimal TotalAmount { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [ForeignKey(nameof(CreditCardId))]
        public virtual CreditCard CreditCard { get; set; }
    }
}
