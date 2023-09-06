using System.ComponentModel.DataAnnotations;

namespace RapidPay.Services.Users.Models
{
    public class UserInput
    {
        [Required]
        public string UserName { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
