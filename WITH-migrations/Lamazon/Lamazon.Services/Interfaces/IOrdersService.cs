using Lamazon.ViewModels.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lamazon.Services.Interfaces
{
    public interface IOrdersService
    {
        Task<List<OrderViewModel>> GetAllOrders();
        Task<OrderViewModel> GetOrderById(int id);
        Task CreateOrder(OrderViewModel orderViewModel);
        Task UpdateOrder(OrderViewModel orderViewModel);
        Task RejectOrder(int id);
        Task AcceptOrder(int id);
        Task<PagedResultViewModel<OrderViewModel>> GetFilteredOrders(DatatableRequestViewModel datatableRequestViewModel);
    }
}
