using AutoMapper;
using Lamazon.DataAccess.Interfaces;
using Lamazon.Services;
using Lamazon.Services.Interfaces;
using Lamazon.ViewModels.Models;
using System.Collections.Generic;

namespace Lamazon.Services.Implementations
{
    public class DashboardService : BaseService, IDashboardService
    {
        private readonly IDashboardRepository _dashboardRepository;
        private readonly IMapper _mapper;

        public DashboardService(IDashboardRepository dashboardRepository, IMapper mapper)
        {
            _dashboardRepository = dashboardRepository;
            _mapper = mapper;
        }

        public DashboardViewModel GetDashboardData()
        {
            var dashboardViewModel = new DashboardViewModel();

            dashboardViewModel.TotalCustomers = _dashboardRepository.CountCustomers();

            dashboardViewModel.TotalOrders = _dashboardRepository.CountOrders();
            dashboardViewModel.TotalOrdersAmount = _dashboardRepository.GetOrdersTotalAmount();
            dashboardViewModel.TotalPendingOrders = _dashboardRepository.CountPendingOrders();
            dashboardViewModel.TotalAcceptedOrders = _dashboardRepository.CountAcceptedOrders();
            dashboardViewModel.TotalRejectedOrders = _dashboardRepository.CountRejectedOrders();

            dashboardViewModel.TotalInvoices = _dashboardRepository.CountInvoices();
            dashboardViewModel.TotalInvoicesAmount = _dashboardRepository.GetInvoicesTotalAmount();
            dashboardViewModel.TotalPendingInvoices = _dashboardRepository.CountPendingInvoices();
            dashboardViewModel.TotalPaidInvoices = _dashboardRepository.CountPaidInvoices();
            dashboardViewModel.TotalCanceledInvoices = _dashboardRepository.CountCanceledInvoices();

            dashboardViewModel.TotalDeletedProducts = _dashboardRepository.CountDeletedProducts();
            dashboardViewModel.TotalActiveProducts = _dashboardRepository.CountActiveProducts();

            var administrators = _dashboardRepository.GetAdministrators();
            dashboardViewModel.Administrators = _mapper.Map<List<UserViewModel>>(administrators);

            return dashboardViewModel;
        }
    }
}