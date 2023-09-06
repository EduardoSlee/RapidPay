using RapidPay.Repositories.Users;
using RapidPay.Services.Users.Models;

namespace RapidPay.Services.Users
{
    public interface IUsersService
    {
        Task<UserResult> GetUserAsync(string userName);
    }
}
