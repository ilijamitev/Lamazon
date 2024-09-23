using Lamazon.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Lamazon.Web.Models;
using Lamazon.Services.Implementations;
using System.Diagnostics;
using System.Linq;

namespace Lamazon.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IProductsService _productsService;

        public HomeController(IProductsService productsService)
        {
            _productsService = productsService;
        }

        public IActionResult Index()
        {
            var shoppingCart = GetShoopingCart();
            var featuredProducts = _productsService.GetAllFeaturedProducts();
            featuredProducts.ForEach(prod =>
            {
                prod.IsAddedToCart = shoppingCart.ShoppingCartItems.Any(cartItem => prod.Id == cartItem.Id);
            });

            return View(featuredProducts);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
