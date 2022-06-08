namespace EcommerceShop.Api.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string message=null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefultMessageWithStatusCode(statusCode);
        }

      
        public int StatusCode { get; set; }
        public string Message { get; set; }

        private string GetDefultMessageWithStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "A bad Reauest ,you have made",
                401 => "Authorized .you are not",
                404 => "Rescource not found ,it was not",
                500 => "Errors are the path to the dark side . Error to lead anger .Anger  lead to head",
                _ => null
            };
        }

    }
}
