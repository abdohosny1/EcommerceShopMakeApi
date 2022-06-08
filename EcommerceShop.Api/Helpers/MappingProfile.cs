

namespace EcommerceShop.Api.Helpers
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(o => o.ProductType, p => p.MapFrom(e => e.ProductType.Name))
                .ForMember(o => o.ProductBrand, p => p.MapFrom(e => e.ProductBrand.Name))
                .ForMember(o => o.PictureUrl, p => p.MapFrom<ProductUrlResolver>());
        }
    }
}
