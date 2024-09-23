using Lamazon.ViewModels.Models;
using System.Collections.Generic;

namespace Lamazon.Services.Interfaces
{
    public interface IProductsService
    {
        List<ProductViewModel> GetAllProducts();
        List<ProductViewModel> GetAllFeaturedProducts();
        PagedResultViewModel<ProductViewModel> GetFilteredProducts(ProductsDatatableRequestViewModel productsDatatableRequestViewModel);
        ProductViewModel GetProductById(int id);
        void CreateProduct(ProductViewModel productViewModel);
        void UpdateProduct(ProductViewModel productViewModel);
        void DeleteProductById(int id);
    }
}
