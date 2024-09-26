using AutoMapper;
using Lamazon.DataAccess.Interfaces;
using Lamazon.Domain.Entities;
using Lamazon.Services;
using Lamazon.Services.Interfaces;
using Lamazon.ViewModels.Models;
using Lamazon.DataAccess;
using System;
using System.Collections.Generic;

namespace Lamazon.Services.Implementations
{
    public class ProductCategoriesService : BaseService, IProductCategoriesService
    {
        private readonly IProductCategoriesRepository _productCategoriesRepository;
        private readonly IMapper _mapper;

        public ProductCategoriesService(IProductCategoriesRepository productCategoriesRepository, IMapper mapper)
        {
            _productCategoriesRepository = productCategoriesRepository;
            _mapper = mapper;
        }

        public void CreateProductCategory(ProductCategoryViewModel productCategoryViewModel)
        {
            var productCategory = _mapper.Map<ProductCategory>(productCategoryViewModel);
            int productCategoryId = _productCategoriesRepository.Insert(productCategory);
            if (productCategoryId <= 0)
            {
                throw new Exception($"Something went wrong while saving the new product category");
            }
        }

        public void DeleteProductCategoryById(int id)
        {
            _productCategoriesRepository.DeleteById(id);
        }

        public List<ProductCategoryViewModel> GetAllProductCategories()
        {
            var productCategories = _productCategoriesRepository.GetAll();
            return _mapper.Map<List<ProductCategoryViewModel>>(productCategories);
        }

        public PagedResultViewModel<ProductCategoryViewModel> GetFilteredProductCategories(DatatableRequestViewModel datatableRequestViewModel)
        {
            var searchValue = datatableRequestViewModel.search?.value ?? string.Empty;
            var productCategoriesPagedResult = _productCategoriesRepository.GetFilteredProductCategories(datatableRequestViewModel.start, datatableRequestViewModel.length, searchValue, datatableRequestViewModel.sortColumn, datatableRequestViewModel.isAscending);
            return _mapper.Map<PagedResultViewModel<ProductCategoryViewModel>>(productCategoriesPagedResult);
        }

        public ProductCategoryViewModel GetProductCategoryById(int id)
        {
            var productCategory = _productCategoriesRepository.GetById(id);
            return _mapper.Map<ProductCategoryViewModel>(productCategory);
        }

        public void UpdateProductCategory(ProductCategoryViewModel productCategoryViewModel)
        {
            var productCategory = _mapper.Map<ProductCategory>(productCategoryViewModel);
            _productCategoriesRepository.Update(productCategory);
        }
    }
}
