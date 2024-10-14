using Lamazon.Domain.Enitities;
using Lamazon.Domain.Models;
using System.Collections.Generic;

namespace Lamazon.DataAccess.Interfaces
{
    public interface IUsersRepository
    {
        List<User> GetAll();
        PagedResultModel<User> GetFilteredUsers(string roleKey, int startIndex, int count, string searchValue, string orderColumn, bool isAscending);
        User GetById(int id);
        User GetByEmail(string email);
        int Insert(User entity);
        void Update(User entity);
        void DeleteById(int id);
        bool HasOtherAdmins(User user);
    }
}
