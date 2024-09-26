using Lamazon.Domain.Entities;
using Lamazon.Domain.Models;
using System.Collections.Generic;

namespace Lamazon.DataAccess.Interfaces
{
    public interface IProductsRepository
    {
        List<Product> GetAll();
        List<Product> GetAllFeaturedProducts();
        PagedResultModel<Product> GetFilteredProducts(int? categoryId, int startIndex, int count, string searchValue, string orderByColumn, bool isAscending);
        Product GetById(int id);
        int Insert(Product product);
        void Update(Product product);
        void DeleteById(int id);
    }
}
