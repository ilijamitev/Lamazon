using Lamazon.Domain.Enitities;
using Lamazon.Domain.Models;
using System.Collections.Generic;

namespace Lamazon.DataAccess.Interfaces
{
    public interface IProductCategoriesRepository
    {
        List<ProductCategory> GetAll();
        PagedResultModel<ProductCategory> GetFilteredProductCategories(int startIdex, int count, string searchValue, string orderByColumn, bool isAscending);
        ProductCategory GetById(int id);
        int Insert(ProductCategory entity);
        void Update(ProductCategory entity);
        void DeleteById(int id);
    }
}
