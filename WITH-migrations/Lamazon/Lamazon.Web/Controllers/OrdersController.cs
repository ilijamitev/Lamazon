using Lamazon.Domain.Constants;
using Lamazon.Services.Interfaces;
using Lamazon.ViewModels.Models;
using Lamazon.Web.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;

namespace Lamazon.Web.Controllers
{
    [Authorize]
    public class OrdersController : BaseController
    {
        private readonly IProductsService _productsService;
        private readonly IGeoTrackerService _geoTrackerService;
        private readonly IOrdersService _ordersService;
        public OrdersController(IProductsService productsService, IGeoTrackerService geoTrackerService, IOrdersService ordersService)
        {
            _productsService = productsService;
            _geoTrackerService = geoTrackerService;
            _ordersService = ordersService;
        }

        public IActionResult ShoppingCart()
        {
            var shoppingCart = GetShoopingCart();

            if (shoppingCart.ShoppingCartItems.Any())
            {
                var orderLineItems = new List<OrderLineItemViewModel>();

                foreach (var shoppingCartItem in shoppingCart.ShoppingCartItems)
                {
                    var product = _productsService.GetProductById(shoppingCartItem.Id);

                    var orderLineItem = new OrderLineItemViewModel();
                    orderLineItem.ProductId = product.Id;
                    orderLineItem.ProductName = product.Name;
                    orderLineItem.ProductPrice = product.DicountedPrice;
                    orderLineItem.ProductDescription = product.Description;
                    orderLineItem.ProductImage = product.ImageUrl;
                    orderLineItem.Quantity = 1;
                    orderLineItem.DiscountPercentage = product.DiscountPercentage;

                    orderLineItem.TotalPrice = orderLineItem.Quantity * orderLineItem.ProductPrice;

                    orderLineItems.Add(orderLineItem);
                }

                return View(orderLineItems);
            }
            else
            {
                return RedirectToAction("Index", "Products");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOrder(List<OrderLineItemViewModel> orderLineItems)
        {
            var orderViewModel = new OrderViewModel();
            orderViewModel.UserId = User.GetUserId();
            orderViewModel.TotalAmount = orderLineItems.Sum(x => x.TotalPrice);
            orderViewModel.OrderLineItems = orderLineItems;

            var ipAddress = Request.HttpContext.Connection.RemoteIpAddress;
            orderViewModel.IpAddress = ipAddress != null ? ipAddress.ToString() : "77.28.200.111";
            var ipGeoInfo = await _geoTrackerService.GetIpGeoInfo(orderViewModel.IpAddress);
            orderViewModel.CountryCode = ipGeoInfo.CountryCode;
            orderViewModel.CountryFlagUrl = await _geoTrackerService.GetCountryFlag(ipGeoInfo.CountryCode);

            await _ordersService.CreateOrder(orderViewModel);

            Response.Cookies.Delete(Cookies.ShoppingCart);
            AddNotificationMessage("Order was successfully created");
            return RedirectToAction("Index", "Home");
        }
    }
}
