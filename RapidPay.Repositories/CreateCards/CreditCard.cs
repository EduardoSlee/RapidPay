using RapidPay.Repositories.Payments;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RapidPay.Repositories.CreateCards
{
    [Table("CreditCards")]
    public class CreditCard
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(15)]
        public string Number { get; set; } = string.Empty;

        [Required]
        public decimal CreditLimit { get; set; }

        [Required]
        public decimal AvailableCredit { get; set; }

        [Required]
        public decimal TotalPayments { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public virtual IEnumerable<Payment>? Payments { get; set; }
    }
}