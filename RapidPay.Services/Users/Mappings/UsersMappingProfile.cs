using AutoMapper;
using RapidPay.Repositories.Users;
using RapidPay.Services.Users.Models;

namespace RapidPay.Services.Users.Mappings
{
    public class UsersMappingProfile : Profile
    {
        public UsersMappingProfile()
        {
            CreateMap<User, UserResult>()
                .ForMember(userResult => userResult.UserRoleName, options =>
                    options.MapFrom(user => user.UserRole.Name));
        }
    }
}
