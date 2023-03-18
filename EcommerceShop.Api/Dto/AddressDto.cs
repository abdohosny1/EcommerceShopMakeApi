
namespace EcommerceShop.Api.Dto
{
    public class AddressDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]

        public string LasttName { get; set; }
        [Required]

        public string Streat { get; set; }
        [Required]


        public string City { get; set; }
        [Required]

        public string State { get; set; }
        [Required]

        public string ZipCode { get; set; }
    }
}
