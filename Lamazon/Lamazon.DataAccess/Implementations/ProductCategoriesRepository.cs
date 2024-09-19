using Lamazon.DataAccess;
using Lamazon.DataAccess.DataContext;
using Lamazon.DataAccess.Extensions;
using Lamazon.DataAccess.Interfaces;
using Lamazon.Domain.Enitities;
using Lamazon.Domain.Enums;
using Lamazon.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lamazon.DataAccess.Implementations
{
    public class ProductCategoriesRepository : BaseRepository, IProductCategoriesRepository
    {
        public ProductCategoriesRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {

        }

        public void DeleteById(int id)
        {
            var productCategory = _applicationDbContext.ProductCategories.FirstOrDefault(x => x.Id == id);
            if (productCategory == null)
            {
                throw new Exception($"ProductCategory with id {id} was not found");
            }

            productCategory.ProductCategoryStatusId = (int)ProductCategoryStatusEnum.Deleted;
            _applicationDbContext.Update(productCategory);
            _applicationDbContext.SaveChanges();
        }

        public List<ProductCategory> GetAll()
        {
            return _applicationDbContext.ProductCategories
                .Where(x => x.ProductCategoryStatusId != (int)ProductCategoryStatusEnum.Deleted).ToList();
        }

        public ProductCategory GetById(int id)
        {
            return _applicationDbContext.ProductCategories
                .FirstOrDefault(x => x.Id == id && x.ProductCategoryStatusId != (int)ProductCategoryStatusEnum.Deleted);
        }

        public PagedResultModel<ProductCategory> GetFilteredProductCategories(int startIdex, int count, string searchValue, string orderByColumn, bool isAscending)
        {
            var result = new PagedResultModel<ProductCategory>();

            var productCategoriesQuery = _applicationDbContext.ProductCategories.Where(x => x.ProductCategoryStatusId != (int)ProductCategoryStatusEnum.Deleted);
            result.TotalRecords = productCategoriesQuery.Count();

            productCategoriesQuery = productCategoriesQuery.Where(x => x.Name.Contains(searchValue));
            result.TotalDisplayRecords = productCategoriesQuery.Count();

            productCategoriesQuery = isAscending ? productCategoriesQuery.OrderByProperty(orderByColumn)
                                                 : productCategoriesQuery.OrderByPropertyDescending(orderByColumn);

            result.Items = productCategoriesQuery.Skip(startIdex).Take(count).ToList();

            return result;
        }

        public int Insert(ProductCategory entity)
        {
            _applicationDbContext.ProductCategories.Add(entity);
            _applicationDbContext.SaveChanges();
            return entity.Id;
        }

        public void Update(ProductCategory entity)
        {
            if (!_applicationDbContext.ProductCategories.Any(x => x.Id == entity.Id && x.ProductCategoryStatusId != (int)ProductCategoryStatusEnum.Deleted))
            {
                throw new Exception($"ProductCategory with id {entity.Id} was not found");
            }

            _applicationDbContext.ProductCategories.Update(entity);
            _applicationDbContext.SaveChanges();
        }
    }
}
