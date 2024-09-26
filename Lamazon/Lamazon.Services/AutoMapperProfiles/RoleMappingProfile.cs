using Lamazon.Domain.Entities;
using Lamazon.ViewModels.Models;

namespace Lamazon.Services.AutoMapperProfiles
{
    public class RoleMappingProfile : AutoMapper.Profile
    {
        public RoleMappingProfile()
        {
            CreateMap<Role, RoleViewModel>().ReverseMap();
        }
    }
}
