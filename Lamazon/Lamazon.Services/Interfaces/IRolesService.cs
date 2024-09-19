using Lamazon.ViewModels.Models;
using System.Collections.Generic;

namespace Lamazon.Services.Interfaces
{
    public interface IRolesService
    {
        List<RoleViewModel> GetAllRoles();
        RoleViewModel GetRoleByKey(string key);
    }
}
