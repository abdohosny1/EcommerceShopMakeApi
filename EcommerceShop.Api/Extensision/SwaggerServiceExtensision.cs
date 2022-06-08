using Microsoft.OpenApi.Models;

namespace EcommerceShop.Api.Extensision
{
    public static class SwaggerServiceExtensision
    {
        public static IServiceCollection AddSwaggerDocumantion(this IServiceCollection services)
        {
            //builder.Services.AddSwaggerGen();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",new OpenApiInfo { Title="SkiNet API",Version="v1"});
            });
            return services;
        }

        public static IApplicationBuilder UseSweggerDoc(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            return app;
               

        }
    }
}
