namespace RapidPay.Services.Users.Models
{
    public class UserResult
    {
        public int Id { get; set; }

        public string UserName { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string UserRoleName { get; set; } = string.Empty;
    }
}
