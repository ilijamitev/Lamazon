using AutoMapper;
using Lamazon.DataAccess.Interfaces;
using Lamazon.Domain.Entities;
using Lamazon.Services.Interfaces;
using Lamazon.ViewModels.Models;
using System.Transactions;

namespace Lamazon.Services.Implementations
{
    public class OrdersService : BaseService, IOrdersService
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly IInvoicesRepository _invoicesRepository;
        private readonly IMapper _mapper;

        public OrdersService(IOrdersRepository ordersRepository, IInvoicesRepository invoicesRepository, IMapper mapper)
        {
            _ordersRepository = ordersRepository;
            _invoicesRepository = invoicesRepository;
            _mapper = mapper;
        }

        public async Task AcceptOrder(int id)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var order = await _ordersRepository.GetById(id);
                    order.OrderStatusId = (int)Domain.Enums.OrderStatusEnum.Accepted;
                    await _ordersRepository.Update(order);

                    var invoice = _mapper.Map<Invoice>(order);
                    var maxId = await _invoicesRepository.GetMaxId();
                    invoice.InvoiceNumber = $"{++maxId}/{DateTime.Now.Year}";
                    await _invoicesRepository.Insert(invoice);

                    scope.Complete();
                }
                catch (Exception ex)
                {
                    LogError(ex.Message, ex);
                    Transaction.Current.Rollback();
                }
            }
        }

        public async Task CreateOrder(OrderViewModel orderViewModel)
        {
            orderViewModel.OrderDate = DateTime.Now;
            orderViewModel.OrderStatus = ViewModels.Enums.OrderStatusEnum.Pending;
            var maxId = await _ordersRepository.GetMaxId();
            orderViewModel.OrderNumber = $"{++maxId}/{DateTime.Now.Year}";

            var order = _mapper.Map<Order>(orderViewModel);
            int orderId = await _ordersRepository.Insert(order);
            if (orderId <= 0)
            {
                throw new Exception($"Something went wrong while saving the new order");
            }
        }

        public async Task<List<OrderViewModel>> GetAllOrders()
        {
            var orders = await _ordersRepository.GetAllOrders();
            return _mapper.Map<List<OrderViewModel>>(orders);
        }

        public async Task<PagedResultViewModel<OrderViewModel>> GetFilteredOrders(DatatableRequestViewModel datatableRequestViewModel)
        {
            var searchValue = datatableRequestViewModel.search?.value ?? string.Empty;
            var ordersPagedResult = await _ordersRepository.GetFilteredOrders(datatableRequestViewModel.start, datatableRequestViewModel.length, searchValue, datatableRequestViewModel.sortColumn, datatableRequestViewModel.isAscending);
            return _mapper.Map<PagedResultViewModel<OrderViewModel>>(ordersPagedResult);
        }

        public async Task<OrderViewModel> GetOrderById(int id)
        {
            var order = await _ordersRepository.GetById(id);
            return _mapper.Map<OrderViewModel>(order);
        }

        public async Task RejectOrder(int id)
        {
            var order = await _ordersRepository.GetById(id);
            order.OrderStatusId = (int)Domain.Enums.OrderStatusEnum.Rejected;
            await _ordersRepository.Update(order);
        }

        public async Task UpdateOrder(OrderViewModel orderViewModel)
        {
            var order = _mapper.Map<Order>(orderViewModel);
            await _ordersRepository.Update(order);
        }
    }
}
