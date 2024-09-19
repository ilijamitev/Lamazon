using Lamazon.ViewModels.Models;
using System.Collections.Generic;

namespace Lamazon.Services.Interfaces
{
    public interface IProductCategoriesService
    {
        List<ProductCategoryViewModel> GetAllProductCategories();
        PagedResultViewModel<ProductCategoryViewModel> GetFilteredProductCategories(DatatableRequestViewModel datatableRequestViewModel);
        ProductCategoryViewModel GetProductCategoryById(int id);
        void CreateProductCategory(ProductCategoryViewModel productCategoryViewModel);
        void UpdateProductCategory(ProductCategoryViewModel productCategoryViewModel);
        void DeleteProductCategoryById(int id);
    }
}
