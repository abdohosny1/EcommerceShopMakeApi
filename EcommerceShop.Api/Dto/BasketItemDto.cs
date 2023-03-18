namespace EcommerceShop.Api.Dto
{
    public class BasketItemDto
    {
        [Required]

        public int Id { get; set; }
        [Required]

        public string ProductName { get; set; }
        [Required]
        [Range(0.1,double.MaxValue,ErrorMessage ="price Must be Grater tgan Zero")]
        public decimal Price { get; set; }


        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Quentity Must be at least one")]
        public int  Quentity { get; set; }


        [Required]
        public string PictureURL { get; set; }
        [Required]

        public string Brand { get; set; }
        [Required]

        public string Type { get; set; }

    }
}
