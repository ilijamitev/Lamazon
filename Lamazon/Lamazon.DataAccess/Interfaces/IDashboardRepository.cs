using Lamazon.Domain.Enitities;
using System.Collections.Generic;

namespace Lamazon.DataAccess.Interfaces
{
    public interface IDashboardRepository
    {
        int CountCustomers();

        int CountOrders();
        int CountPendingOrders();
        int CountAcceptedOrders();
        int CountRejectedOrders();
        decimal GetOrdersTotalAmount();

        int CountInvoices();
        int CountPendingInvoices();
        int CountPaidInvoices();
        int CountCanceledInvoices();
        decimal GetInvoicesTotalAmount();


        int CountActiveProducts();
        int CountDeletedProducts();


        List<User> GetAdministrators();
    }
}
