using AutoMapper;
using Lamazon.DataAccess.Interfaces;
using Lamazon.Services;
using Lamazon.Services.Interfaces;
using Lamazon.ViewModels.Models;
using System.Collections.Generic;

namespace Lamazon.Services.Implementations
{
    public class RolesService : BaseService, IRolesService
    {
        private readonly IRolesRepository _rolesRepository;
        private readonly IMapper _mapper;

        public RolesService(IRolesRepository rolesRepository, IMapper mapper)
        {
            _rolesRepository = rolesRepository;
            _mapper = mapper;
        }

        public List<RoleViewModel> GetAllRoles()
        {
            var roles = _rolesRepository.GetAll();
            return _mapper.Map<List<RoleViewModel>>(roles);
        }

        public RoleViewModel GetRoleByKey(string key)
        {
            var role = _rolesRepository.GetRoleByKey(key);
            return _mapper.Map<RoleViewModel>(role);
        }
    }
}
