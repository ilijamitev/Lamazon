using Lamazon.Domain.Constants;
using Lamazon.Services.Interfaces;
using Lamazon.ViewModels.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Lamazon.Web.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Authorize(Roles = Roles.Admin)]
    public class ProductsController : ControllerBase
    {
        private IProductsService _productsService;
        private IProductCategoriesService _productCategoriesService;

        public ProductsController(IProductsService productsService, IProductCategoriesService productCategoriesService)
        {
            _productsService = productsService;
            _productCategoriesService = productCategoriesService;

            PageName = "Products";
        }

        public IActionResult Index()
        {
            SetMetadata();
            return View();
        }

        [HttpPost]
        public JsonResult GetProducts(ProductsDatatableRequestViewModel roductsDatatableRequestViewModel)
        {
            var pagedResult = _productsService.GetFilteredProducts(roductsDatatableRequestViewModel);
            return Json(pagedResult.ToTableData());
        }

        public IActionResult Create()
        {
            var productViewModel = new ProductViewModel();
            SetMetadata(productViewModel);
            return View(productViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                _productsService.CreateProduct(productViewModel);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                SetMetadata(productViewModel);
                return View(productViewModel);
            }

        }

        public IActionResult Edit(int? id)
        {
            if (id.HasValue)
            {
                var productViewModel = _productsService.GetProductById(id.Value);
                SetMetadata(productViewModel);
                return View(productViewModel);
            }
            else
            {
                return new EmptyResult();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                _productsService.UpdateProduct(productViewModel);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                SetMetadata(productViewModel);
                return View(productViewModel);
            }

        }

        public IActionResult Delete(int? id)
        {
            if (id.HasValue)
            {
                var productViewModel = _productsService.GetProductById(id.Value);
                SetMetadata(productViewModel);
                return View(productViewModel);
            }
            else
            {
                return new EmptyResult();
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int? id)
        {
            if (id.HasValue)
            {
                _productsService.DeleteProductById(id.Value);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return new EmptyResult();
            }
        }

        private void SetMetadata(ProductViewModel productViewModel = null)
        {
            var productCategories = _productCategoriesService.GetAllProductCategories();
            ViewBag.ProductCategoryList = new SelectList(productCategories, "Id", "Name", productViewModel?.ProductCategoryId);
        }
    }
}
