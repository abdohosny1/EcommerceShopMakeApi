using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;

namespace EcommerceShop.Api.Helpers
{
    public class CachedAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int _timeToLiveSeconed;

        public CachedAttribute(int timeToLiveSeconed)
        {
            _timeToLiveSeconed = timeToLiveSeconed;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var casheService =
                context.HttpContext.RequestServices.GetRequiredService<IResponseCasheService>();

            var casheKey= GenerateCasheKeyFromRequest(context.HttpContext.Request);

            var cashedResponse=await casheService.GetCasheResponseAsync(casheKey);

            if (!string.IsNullOrEmpty(cashedResponse))
            {
                var contentResult = new ContentResult
                {
                    Content = cashedResponse,
                    ContentType = "application/json",
                    StatusCode = 200
                };

                context.Result= contentResult;
                return;
            }

            var executedContext = await next();

            if(executedContext.Result  is OkObjectResult okResult)
            {
                await casheService.CasheResponseAsync(casheKey,okResult.Value,TimeSpan.FromSeconds(_timeToLiveSeconed));
            }
        }

        private string GenerateCasheKeyFromRequest(HttpRequest request)
        {
            var keyBulider = new StringBuilder();

            keyBulider.Append($"{request.Path}");

            foreach (var (key,value) in request.Query.OrderBy(x=>x.Key))
            {
                keyBulider.Append($"|{key}-{value}");
            }

            return keyBulider.ToString();
        }
    }
}
