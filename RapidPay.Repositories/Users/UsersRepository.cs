using Microsoft.EntityFrameworkCore;
using RapidPay.Repositories.Data;

namespace RapidPay.Repositories.Users
{
    public class UsersRepository : IUsersRepository
    {
        private readonly RapidPayDbContext _rapidPayDbContext;

        public UsersRepository(RapidPayDbContext rapidPayDbContext)
        {
            _rapidPayDbContext = rapidPayDbContext;
        }

        public async Task<User?> GetUserAsync(string userName)
        {
            return await _rapidPayDbContext.Users
                .Include(user => user.UserRole)
                .FirstOrDefaultAsync(user => user.UserName == userName);
        }
    }
}
