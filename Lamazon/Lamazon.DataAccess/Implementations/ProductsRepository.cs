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
    public class ProductsRepository : BaseRepository, IProductsRepository
    {
        public ProductsRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext) { }

        public void DeleteById(int id)
        {
            var product = _applicationDbContext.Products.FirstOrDefault(x => x.Id == id);
            if (product == null)
            {
                throw new Exception($"Product with id {id} was not found");
            }

            product.ProductStatusId = (int)ProductStatusEnum.Deleted;
            _applicationDbContext.Update(product);
            _applicationDbContext.SaveChanges();
        }

        public List<Product> GetAll()
        {
            return _applicationDbContext.Products.Include(x => x.ProductCategory)
                .Where(x => x.ProductStatusId != (int)ProductStatusEnum.Deleted)
                .ToList();
        }

        public PagedResultModel<Product> GetFilteredProducts(int? categoryId, int startIndex, int count, string searchValue, string orderByColumn, bool isAscending)
        {
            var result = new PagedResultModel<Product>();

            var productsQuery = _applicationDbContext.Products.Include(x => x.ProductCategory).Where(x => x.ProductStatusId != (int)ProductStatusEnum.Deleted);
            result.TotalRecords = productsQuery.Count();

            if (categoryId.HasValue)
            {
                productsQuery = productsQuery.Where(x => x.ProductCategoryId == categoryId);
            }

            productsQuery = productsQuery.Where(x => x.Name.Contains(searchValue));
            result.TotalDisplayRecords = productsQuery.Count();

            productsQuery = isAscending ? productsQuery.OrderByProperty(orderByColumn)
                                        : productsQuery.OrderByPropertyDescending(orderByColumn);
            result.Items = productsQuery.ToList();

            return result;
        }

        public Product GetById(int id)
        {
            return _applicationDbContext.Products
                .Include(x => x.ProductCategory)
                .FirstOrDefault(x => x.Id == id && x.ProductStatusId != (int)ProductStatusEnum.Deleted);
        }

        public int Insert(Product product)
        {
            _applicationDbContext.Products.Add(product);
            _applicationDbContext.SaveChanges();
            return product.Id;
        }

        public void Update(Product product)
        {
            if (_applicationDbContext.Products.Any(x => x.Id == product.Id && x.ProductStatusId != (int)ProductStatusEnum.Deleted))
            {
                _applicationDbContext.Update(product);
                _applicationDbContext.SaveChanges();
            }
            else
            {
                throw new Exception($"Product with id {product.Id} was not found");
            }
        }

        public List<Product> GetAllFeaturedProducts()
        {
            return _applicationDbContext.Products
                .Include(x => x.ProductCategory)
                .Where(x => x.IsFeatured && x.ProductStatusId != (int)ProductStatusEnum.Deleted)
                .ToList();
        }
    }
}
