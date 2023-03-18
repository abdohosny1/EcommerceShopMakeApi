namespace EcommerceShop.Api.Dto
{
    public class OrderDto
    {
        public string baskeyId { get; set; }
        public int DeliveryMethodId { get; set; }
        public AddressDto ShippingAddress { get; set; }
    }
}
