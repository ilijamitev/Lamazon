using Lamazon.Domain.Enitities;
using Lamazon.Domain.Models;
using Lamazon.ViewModels.Models;

namespace Lamazon.Services.AutoMapperProfiles
{
    public class ProductMappingProfile : AutoMapper.Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<Product, ProductViewModel>()
                .ForMember(x => x.ProductStatus, opt => opt.Ignore())
                .ForMember(x => x.ProductStatus, opt => opt.MapFrom(x => x.ProductStatusId))
                .ForMember(x => x.Info, opt => opt.MapFrom(s => $"{s.Id.ToString("000")} - {s.Name} ({s.ProductCategory.Name})"))
                .ReverseMap()
                .ForMember(x => x.ProductStatus, opt => opt.Ignore())
                .ForMember(x => x.ProductStatusId, opt => opt.MapFrom(x => x.ProductStatus));

            CreateMap<PagedResultModel<Product>, PagedResultViewModel<ProductViewModel>>();
        }
    }
}
