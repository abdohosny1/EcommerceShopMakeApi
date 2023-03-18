using EcommerceShop.Core.Model.OrderAggragate;

namespace EcommerceShop.Api.Helpers
{
    public class OrderItemUrlResolver : IValueResolver<OrderItem, OrderItemDto, string>
    {
        private readonly IConfiguration configuration;

        public OrderItemUrlResolver(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.ItemOrdered.ProductUrl))
            {
                return configuration["ApiUrl"] + source.ItemOrdered.ProductUrl;
            }
            return null;
        }
    }
}

