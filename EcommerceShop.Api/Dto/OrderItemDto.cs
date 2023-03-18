using EcommerceShop.Core.Model.OrderAggragate;

namespace EcommerceShop.Api.Dto
{
    public class OrderItemDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string PicturalURL { get; set; }

        public decimal Price { get; set; }

        public int Quentity { get; set; }
    }
}
