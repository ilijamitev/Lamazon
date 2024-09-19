using Lamazon.Domain.Enitities;
using Lamazon.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lamazon.DataAccess.Interfaces
{
    public interface IInvoicesRepository
    {
        Task<List<Invoice>> GetAllInvoices();
        Task<Invoice> GetById(int id);
        Task<int> Insert(Invoice invoice);
        Task Update(Invoice invoice);
        Task<int> GetMaxId();
        Task<PagedResultModel<Invoice>> GetFilteredInvoices(int startIndex, int count, string searchValue, string orderByColumn, bool isAscending);
    }
}
