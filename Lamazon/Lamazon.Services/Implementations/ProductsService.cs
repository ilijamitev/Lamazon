using AutoMapper;
using Lamazon.DataAccess.Interfaces;
using Lamazon.Services;
using Lamazon.Services.Interfaces;
using Lamazon.ViewModels.Models;
using Lamazon.DataAccess;
using System;
using System.Collections.Generic;
using Lamazon.Domain.Enitities;

namespace Lamazon.Services.Implementations
{
    public class ProductsService : BaseService, IProductsService
    {
        private readonly IProductsRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductsService(IProductsRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public void CreateProduct(ProductViewModel productViewModel)
        {
            var product = _mapper.Map<Product>(productViewModel);
            int productId = _productRepository.Insert(product);
            if (productId <= 0)
            {
                throw new Exception($"Something went wrong while saving the new product");
            }
        }

        public void DeleteProductById(int id)
        {
            _productRepository.DeleteById(id);
        }

        public List<ProductViewModel> GetAllProducts()
        {
            var products = _productRepository.GetAll();
            return _mapper.Map<List<ProductViewModel>>(products);
        }

        public List<ProductViewModel> GetAllFeaturedProducts()
        {
            var featuredProducts = _productRepository.GetAllFeaturedProducts();
            return _mapper.Map<List<ProductViewModel>>(featuredProducts);
        }

        public PagedResultViewModel<ProductViewModel> GetFilteredProducts(ProductsDatatableRequestViewModel productsDatatableRequestViewModel)
        {
            LogInfo("Nekoja test poraka so info sto nesto kje ni znaci koga kje go procitame.");

            var searchValue = productsDatatableRequestViewModel.search?.value ?? string.Empty;
            var productCategoriesPagedResult = _productRepository.GetFilteredProducts(productsDatatableRequestViewModel.CategoryId, productsDatatableRequestViewModel.start, productsDatatableRequestViewModel.length, searchValue, productsDatatableRequestViewModel.sortColumn, productsDatatableRequestViewModel.isAscending);
            return _mapper.Map<PagedResultViewModel<ProductViewModel>>(productCategoriesPagedResult);
        }

        public ProductViewModel GetProductById(int id)
        {
            var product = _productRepository.GetById(id);
            return _mapper.Map<ProductViewModel>(product);
        }

        public void UpdateProduct(ProductViewModel productViewModel)
        {
            var product = _mapper.Map<Product>(productViewModel);
            _productRepository.Update(product);
        }
    }
}
