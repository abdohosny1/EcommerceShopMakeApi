using EcommerceShop.Core.Model.sendingEmail;
using EcommerceShop.EF.Services;

namespace EcommerceShop.Api.Extensision
{
    public static class ApplicationServicExtensision
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddSingleton<IResponseCasheService, ResponseCasheService>();
            services.AddTransient<ISMSService, SMSService>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IPaymentServices, PaymentServices>();
            services.AddScoped<IPaymentServices, PaymentServices>();
            services.AddScoped<IPaymentServices, PaymentServices>();
            services.AddTransient<IMailSendService, MailSendService>();
            services.AddScoped<IUnutOfWork,UnutOfWork>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IOrderService, OrderService>();


            services.Configure<ApiBehaviorOptions>(
          op => {
        op.InvalidModelStateResponseFactory = actionContext =>
        {
            var errors = actionContext.ModelState
                       .Where(e => e.Value.Errors.Count > 0)
                       .SelectMany(x => x.Value.Errors)
                       .Select(x => x.ErrorMessage).ToArray();

            var errorResponse = new ApiValidationErrorResponse
            {
                Errors = errors
            };
            return new BadRequestObjectResult(errorResponse);
        };
    });
            return services;
        }
    }
}
