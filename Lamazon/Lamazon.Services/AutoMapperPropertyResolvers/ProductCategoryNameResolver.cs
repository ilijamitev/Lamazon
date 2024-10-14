using AutoMapper;
using Lamazon.Domain.Enitities;
using Lamazon.Services.Interfaces;
using Lamazon.ViewModels.Models;

namespace Lamazon.Services.AutoMapperPropertyResolvers
{
    public class ProductCategoryNameResolver : IValueResolver<ProductCategory, ProductCategoryViewModel, string>
    {
        private readonly ILocalizationService _localizationService;

        public ProductCategoryNameResolver(ILocalizationService localizationService)
        {
            _localizationService = localizationService;
        }

        public string Resolve(ProductCategory source, ProductCategoryViewModel destination, string destMember, ResolutionContext context)
        {
            return _localizationService.LocalizeString(source.Name);
        }
    }
}
