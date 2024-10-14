using Lamazon.Domain.Constants;
using Lamazon.Services.Interfaces;
using Lamazon.ViewModels.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lamazon.Web.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Authorize(Roles = Roles.Admin)]
    public class ProductCategoriesController : ControllerBase
    {
        private IProductCategoriesService _productCategoriesService;

        public ProductCategoriesController(IProductCategoriesService productCategoriesService)
        {
            _productCategoriesService = productCategoriesService;
            PageName = "Product Categories";
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetProductCategories(DatatableRequestViewModel datatableRequestViewModel)
        {
            var pagedResult = _productCategoriesService.GetFilteredProductCategories(datatableRequestViewModel);
            return Json(pagedResult.ToTableData());
        }

        public IActionResult Create()
        {
            var productCategoryViewModel = new ProductCategoryViewModel();
            return View(productCategoryViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductCategoryViewModel productCategoryViewModel)
        {
            if (ModelState.IsValid)
            {
                _productCategoriesService.CreateProductCategory(productCategoryViewModel);
                return RedirectToAction(nameof(Index));
            }
            return View(productCategoryViewModel);
        }


        public IActionResult Edit(int? id)
        {
            if (id.HasValue)
            {
                var productCategoryViewModel = _productCategoriesService.GetProductCategoryById(id.Value);
                return View(productCategoryViewModel);
            }
            return new EmptyResult();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProductCategoryViewModel productCategoryViewModel)
        {
            if (ModelState.IsValid)
            {
                _productCategoriesService.UpdateProductCategory(productCategoryViewModel);
                return RedirectToAction(nameof(Index));
            }
            return View(productCategoryViewModel);
        }

        public IActionResult Delete(int? id)
        {
            if (id.HasValue)
            {
                var productCategoryViewModel = _productCategoriesService.GetProductCategoryById(id.Value);
                return View(productCategoryViewModel);
            }
            return new EmptyResult();
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int? id)
        {
            if (id.HasValue)
            {
                _productCategoriesService.DeleteProductCategoryById(id.Value);
                return RedirectToAction(nameof(Index));
            }
            return new EmptyResult();
        }
    }
}
