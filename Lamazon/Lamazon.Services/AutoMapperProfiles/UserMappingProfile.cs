using Lamazon.Domain.Enitities;
using Lamazon.Domain.Models;
using Lamazon.ViewModels.Models;

namespace Lamazon.Services.AutoMapperProfiles
{
    public class UserMappingProfile : AutoMapper.Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, UserViewModel>().ReverseMap();

            CreateMap<User, RegisterUserViewModel>().ReverseMap();

            CreateMap<PagedResultModel<User>, PagedResultViewModel<UserViewModel>>();
        }
    }
}
