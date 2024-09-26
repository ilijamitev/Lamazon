using Lamazon.DataAccess.DataContext;
using Lamazon.DataAccess.Interfaces;
using Lamazon.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lamazon.DataAccess.Implementations
{
    public class RolesRepository : BaseRepository, IRolesRepository
    {
        public RolesRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext) { }

        public List<Role> GetAll()
        {
            return _applicationDbContext.Roles.ToList();
        }

        public Role GetRoleByKey(string key)
        {
            return _applicationDbContext.Roles.FirstOrDefault(x => x.Key == key);
        }
    }
}
