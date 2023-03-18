

using EcommerceShop.Core.Model.OrderAggragate;

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

            CreateMap<Core.Model.identity.Address, AddressDto>().ReverseMap();
            CreateMap<BasketItemDto, BasketItem>();
            CreateMap<BasketDto, CustomerBasket>();
            CreateMap<AddressDto, EcommerceShop.Core.Model.OrderAggragate.Address>();
            CreateMap<EcommerceShop.Core.Model.OrderAggragate.Order, OrderToReturnDTO>()
                .ForMember(o => o.DeliveryMethods, o => o.MapFrom(s => s.DeliveryMethods.ShortName))
                .ForMember(o => o.ShippingPrice, o => o.MapFrom(s => s.DeliveryMethods.Price));

            CreateMap<OrderItem, OrderItemDto>()
                   .ForMember(o => o.ProductId, o => o.MapFrom(s => s.ItemOrdered.ProductItemId))
                   .ForMember(o => o.ProductName, o => o.MapFrom(s => s.ItemOrdered.PrpductName))
                   .ForMember(o => o.PicturalURL, o => o.MapFrom(s => s.ItemOrdered.ProductUrl))
                   .ForMember(o => o.PicturalURL, o => o.MapFrom<OrderItemUrlResolver>());

        }
    }
}
