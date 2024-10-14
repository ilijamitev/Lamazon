using Lamazon.DataAccess.DataContext;
using Lamazon.DataAccess.Extensions;
using Lamazon.DataAccess.Interfaces;
using Lamazon.Domain.Constants;
using Lamazon.Domain.Enitities;
using Lamazon.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lamazon.DataAccess.Implementations
{
    public class UsersRepository : BaseRepository, IUsersRepository
    {
        public UsersRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext) { }

        public void DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public List<User> GetAll()
        {
            return _applicationDbContext.Users.Include(x => x.Role).ToList();
        }

        public User GetByEmail(string email)
        {
            return _applicationDbContext.Users
                .Include(x => x.Role)
                .FirstOrDefault(x => x.Email == email);
        }

        public User GetById(int id)
        {
            return _applicationDbContext.Users
                .Include(x => x.Role)
                .FirstOrDefault(x => x.Id == id);
        }

        public PagedResultModel<User> GetFilteredUsers(string roleKey, int startIndex, int count, string searchValue, string orderColumn, bool isAscending)
        {
            var result = new PagedResultModel<User>();

            var usersQuery = _applicationDbContext.Users.Include(x => x.Role).AsQueryable();
            result.TotalRecords = usersQuery.Count();

            if (!string.IsNullOrEmpty(roleKey))
            {
                usersQuery = usersQuery.Where(x => x.RoleKey == roleKey);
            }

            usersQuery = usersQuery.Where(x => x.FullName.Contains(searchValue) || x.Email.Contains(searchValue));
            result.TotalDisplayRecords = usersQuery.Count();

            usersQuery = isAscending ? usersQuery.OrderByProperty(orderColumn)
                                     : usersQuery.OrderByPropertyDescending(orderColumn);

            result.Items = usersQuery.ToList();
            return result;
        }

        public bool HasOtherAdmins(User user)
        {
            return _applicationDbContext.Users.Any(x => x.Id != user.Id && x.RoleKey == Roles.Admin);
        }

        public int Insert(User entity)
        {
            _applicationDbContext.Users.Add(entity);
            _applicationDbContext.SaveChanges();
            return entity.Id;
        }

        public void Update(User entity)
        {
            if (!_applicationDbContext.Users.Any(x => x.Id == entity.Id))
            {
                throw new Exception($"User with id {entity.Id} was not found.");
            }

            _applicationDbContext.Users.Update(entity);
            _applicationDbContext.SaveChanges();
        }
    }
}
