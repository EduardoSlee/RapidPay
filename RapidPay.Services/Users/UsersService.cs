using AutoMapper;
using RapidPay.Repositories.Users;
using RapidPay.Services.Users.Models;

namespace RapidPay.Services.Users
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;

        private readonly IMapper _mapper;

        public UsersService(IUsersRepository usersRepository, IMapper mapper)
        {
            _usersRepository = usersRepository;
            _mapper = mapper;
        }

        public async Task<UserResult> GetUserAsync(string userName)
        {
            var user = await _usersRepository.GetUserAsync(userName);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found.");
            }

            return _mapper.Map<UserResult>(user);
        }
    }
}
