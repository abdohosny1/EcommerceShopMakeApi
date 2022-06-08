 namespace EcommerceShop.Api.Errors
{
    public class ApiExpection : ApiResponse
    {
        public ApiExpection(int statusCode, string message = null,string details=null) : base(statusCode, message)
        {
            Details = details;
        }

        public  string Details { get; set; }
    }
}
