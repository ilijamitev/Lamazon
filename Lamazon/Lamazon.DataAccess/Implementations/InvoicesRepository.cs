using Lamazon.DataAccess.DataContext;
using Lamazon.DataAccess.Extensions;
using Lamazon.DataAccess.Interfaces;
using Lamazon.Domain.Entities;
using Lamazon.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Lamazon.DataAccess.Implementations
{
    public class InvoicesRepository : BaseRepository, IInvoicesRepository
    {
        public InvoicesRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext) { }

        public async Task<List<Invoice>> GetAllInvoices()
        {
            return await _applicationDbContext.Invoices.ToListAsync();
        }

        public async Task<Invoice> GetById(int id)
        {
            return await _applicationDbContext.Invoices
                .Include(x => x.InvoiceLineItems)
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<PagedResultModel<Invoice>> GetFilteredInvoices(int startIndex, int count, string searchValue, string orderByColumn, bool isAscending)
        {
            var result = new PagedResultModel<Invoice>();

            var invoicesQuery = _applicationDbContext.Invoices.Include(x => x.User).AsQueryable();
            result.TotalRecords = invoicesQuery.Count();


            invoicesQuery = invoicesQuery.Where(x => x.InvoiceNumber.Contains(searchValue));
            result.TotalDisplayRecords = invoicesQuery.Count();

            invoicesQuery = isAscending ? invoicesQuery.OrderByProperty(orderByColumn)
                                        : invoicesQuery.OrderByPropertyDescending(orderByColumn);
            result.Items = await invoicesQuery.Skip(startIndex).Take(count).ToListAsync();

            return result;
        }

        public async Task<int> GetMaxId()
        {
            if (await _applicationDbContext.Invoices.AnyAsync())
            {
                return _applicationDbContext.Invoices.Max(x => x.Id);
            }
            else
            {
                return 0;
            }
        }

        public async Task<int> Insert(Invoice invoice)
        {
            await _applicationDbContext.Invoices.AddAsync(invoice);
            await _applicationDbContext.SaveChangesAsync();
            return invoice.Id;
        }

        public async Task Update(Invoice invoice)
        {
            if (await _applicationDbContext.Invoices.AnyAsync(x => x.Id == invoice.Id))
            {
                _applicationDbContext.Update(invoice);
                await _applicationDbContext.SaveChangesAsync();
            }
            else
            {
                throw new Exception($"Invoice with id {invoice.Id} was not found");
            }
        }
    }
}
