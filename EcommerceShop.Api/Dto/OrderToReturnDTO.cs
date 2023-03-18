using EcommerceShop.Core.Model.OrderAggragate;

namespace EcommerceShop.Api.Dto
{
    public class OrderToReturnDTO
    {
        public int Id { get; set; }
        public string BuyerEmail { get; set; }

        public Address ShipToAddress { get; set; }

        public DateTimeOffset OrderDate { get; set; }

        public string DeliveryMethods { get; set; }

        public decimal ShippingPrice { get; set; }

        public IReadOnlyList<OrderItemDto> OrderItems { get; set; }

        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }

        public string OrderStatus { get; set; } 

    }
}
