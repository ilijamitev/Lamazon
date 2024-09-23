using Lamazon.ViewModels.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lamazon.Services.Interfaces
{
    public interface IInvoicesService
    {
        Task<List<InvoiceViewModel>> GetAllInvoices();
        Task<PagedResultViewModel<InvoiceViewModel>> GetFilteredInvoices(DatatableRequestViewModel datatableRequestViewModel);
        Task<InvoiceViewModel> GetInvoiceById(int id);
        Task CreateInvoice(InvoiceViewModel invoiceViewModel);
        Task UpdateInvoice(InvoiceViewModel invoiceViewModel);
        Task CancelInvoice(int id);
        Task SetAsPaid(int id);
    }
}
