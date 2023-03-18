namespace EcommerceShop.Api.Dto
{
    public class MailRequestDTO
    {
        [Required]
        public string tOEmail { get; set; }
        [Required]

        public string Subject { get; set; }
        [Required]

        public string Body { get; set; }
      //  public IList<IFormFile> Attachment { get; set; }
    }
}
