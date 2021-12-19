using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace tournament.Configurations
{
    public static class ConfigurationExtensionMethods
    {
        public static IServiceCollection UseSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("demo", new Info { Title = "Tournament system", Version = "v1" });
            });

            return services;
        }

        public static IApplicationBuilder ConfigureSwagger(this IApplicationBuilder app)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                //                c.SwaggerEndpoint("/swagger/v1/swagger.json", "version_demo");
                c.SwaggerEndpoint("/swagger/demo/swagger.json", "Example");
                c.RoutePrefix = "academy";
            });
            return app;
        }

    }
}
