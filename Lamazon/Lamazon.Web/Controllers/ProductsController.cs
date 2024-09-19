using Lamazon.Domain.Constants;
using Lamazon.Services.Interfaces;
using Lamazon.ViewModels.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace Lamazon.Web.Controllers
{
    public class ProductsController : BaseController
    {
        private IProductsService _productsServices;
        public ProductsController(IProductsService productsServices)
        {
            _productsServices = productsServices;
        }

        public IActionResult Index()
        {
            var shoppingCart = GetShoopingCart();
            var allProducts = _productsServices.GetAllProducts();
            allProducts.ForEach(prod =>
            {
                prod.IsAddedToCart = shoppingCart.ShoppingCartItems.Any(cartItem => prod.Id == cartItem.Id);
            });
            return View(allProducts);
        }

        public IActionResult ProductDetails(int? id)
        {
            if (id.HasValue)
            {
                var product = _productsServices.GetProductById(id.Value);
                return View(product);
            }
            else
            {
                return new EmptyResult();
            }
        }

        [HttpPost]
        public JsonResult AddToCart(ShoppingCartItemViewModel shoppingCartItemViewModel)
        {
            var shoppingCartItemsCount = AddShoppingCartItem(shoppingCartItemViewModel);
            return Json(new { ItemsCount = shoppingCartItemsCount });
        }


        private int AddShoppingCartItem(ShoppingCartItemViewModel shoppingCartItemViewModel)
        {
            var shoppingCart = GetShoopingCart();
            if (!shoppingCart.ShoppingCartItems.Any(x => x.Id == shoppingCartItemViewModel.Id))
            {
                shoppingCart.ShoppingCartItems.Add(shoppingCartItemViewModel);
            }

            //Cookie MAX Size 4096B = 4KB
            Response.Cookies.Append(Cookies.ShoppingCart, JsonConvert.SerializeObject(shoppingCart), new CookieOptions { Expires = DateTime.Now.AddDays(2) });
            return shoppingCart.ShoppingCartItems.Count;
        }

        [HttpPost]
        public JsonResult RemoveFromCart(ShoppingCartItemViewModel shoppingCartItemViewModel)
        {
            var shoppingCartItemsCount = RemoveFromShoppingCart(shoppingCartItemViewModel);
            return Json(new { ItemsCount = shoppingCartItemsCount });
        }

        private int RemoveFromShoppingCart(ShoppingCartItemViewModel shoppingCartItemViewModel)
        {
            var shoppingCart = GetShoopingCart();
            var cartItem = shoppingCart.ShoppingCartItems.FirstOrDefault(x => x.Id == shoppingCartItemViewModel.Id);
            if (cartItem != null)
            {
                shoppingCart.ShoppingCartItems.Remove(cartItem);
            }

            if (shoppingCart.ShoppingCartItems.Any())
            {
                Response.Cookies.Append(Cookies.ShoppingCart, JsonConvert.SerializeObject(shoppingCart), new CookieOptions { Expires = DateTime.Now.AddDays(2) });
            }
            else
            {
                Response.Cookies.Delete(Cookies.ShoppingCart);
            }

            return shoppingCart.ShoppingCartItems.Count;
        }
    }
}
