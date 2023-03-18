namespace EcommerceShop.Api.Dto
{
    public class BasketDto
    {
        [Required]
        public string Id { get; set; }


        public List<BasketItemDto> basketItems { get; set; }

        public int? DeliveryMethodId { get; set; }
        public string? ClientSecret { get; set; }
        public string? PaymentIntentedId { get; set; }
        public decimal? shippingPrice { get; set; }
    }
}
