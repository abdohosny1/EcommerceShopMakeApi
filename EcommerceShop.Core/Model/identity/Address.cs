namespace EcommerceShop.Core.Model.identity
{
    public class Address
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LasttName { get; set; }
        public string Streat { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string ZipCode { get; set; }

        [Required]
        public string AppUserId{ get; set; }

        public AppUser AppUser { get; set; }






    }
}