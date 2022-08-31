namespace EcommerceShop.Api.Extensision
{
    public static class ApplicationServicExtensision
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IBasketRepository, BasketRepository>();


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
