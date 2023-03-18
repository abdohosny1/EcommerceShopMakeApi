namespace EcommerceShop.Api.Dto
{
    public class SendTempDto
    {
        [Required]
        public string UserName { get; set; }

        //[Required]
        //public List<BasketItemDto> basketItems { get; set; }


        [Required]

        public int OrderNum { get; set; }

        [Required]

        public string OrderDate { get; set; }

        [Required]

        public int OrderTotal { get; set; }

        [Required]

        public string Email { get; set; }

        [Required]

        public int SubTotal { get; set; }

        [Required]

        public int shippingPrice { get; set; }

        //[Required]
        //public string Address { get; set; }



    }
}
