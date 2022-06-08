using EcommerceShop.Api.Errors;
using System.Net;
using System.Text.Json;

namespace EcommerceShop.Api.MiddleWare
{
    public class ExpectionMiddelWare
    {

        public ExpectionMiddelWare(RequestDelegate next,ILogger<ExpectionMiddelWare> logger , IHostEnvironment environment)
        {
            Next = next;
            Logger = logger;
            Environment = environment;
        }

        private readonly RequestDelegate Next;

        public ILogger<ExpectionMiddelWare> Logger { get; }
        public IHostEnvironment Environment { get; }


        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                 await Next(context);

            }catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response = Environment.IsDevelopment()
                    ? new ApiExpection((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString())
                    : new ApiExpection((int)HttpStatusCode.InternalServerError);

                var opation = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

                var json = JsonSerializer.Serialize(response,opation);

                await context.Response.WriteAsync(json);

            }

        }
    }
}
