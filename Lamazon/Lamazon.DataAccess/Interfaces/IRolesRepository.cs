using Lamazon.Domain.Enitities;
using System.Collections.Generic;

namespace Lamazon.DataAccess.Interfaces
{
    public interface IRolesRepository
    {
        List<Role> GetAll();
        Role GetRoleByKey(string key);
    }
}
