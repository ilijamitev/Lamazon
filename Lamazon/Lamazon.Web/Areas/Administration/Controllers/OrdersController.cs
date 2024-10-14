using Lamazon.Domain.Constants;
using Lamazon.Services.Interfaces;
using Lamazon.ViewModels.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lamazon.Web.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Authorize(Roles = Roles.Admin)]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersService _ordersService;

        public OrdersController(IOrdersService ordersService)
        {
            _ordersService = ordersService;
            PageName = "Orders";
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> GetOrders(DatatableRequestViewModel datatableRequestViewModel)
        {
            var pagedResult = await _ordersService.GetFilteredOrders(datatableRequestViewModel);
            return Json(pagedResult.ToTableData());
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id.HasValue)
            {
                var orderViewModel = await _ordersService.GetOrderById(id.Value);
                return View(orderViewModel);
            }
            return new EmptyResult();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RejectOrder(int? id)
        {
            if (id.HasValue)
            {
                await _ordersService.RejectOrder(id.Value);
                AddNotificationMessage("Order was rejected");
                return RedirectToAction(nameof(Index));
            }
            return new EmptyResult();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AcceptOrder(int? id)
        {
            if (id.HasValue)
            {
                await _ordersService.AcceptOrder(id.Value);
                AddNotificationMessage("Order was accepted");
                return RedirectToAction(nameof(Index));
            }
            return new EmptyResult();
        }
    }
}
