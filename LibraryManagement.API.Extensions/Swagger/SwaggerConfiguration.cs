using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace LibraryManagement.API.Extensions.Swagger
{
    public static class SwaggerConfiguration
    {
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("swagger", new OpenApiInfo
                {
                    Version = "v1",                 
                    Title = "Library Management API",
                    Description = "Library Management API"
                });
            });
        }
        public static void UseLMSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(conf =>
            {
                conf.SwaggerEndpoint($"swagger/swagger.json", "Library Management");
                conf.DocExpansion(DocExpansion.Full);
            });
        }
    }
}
