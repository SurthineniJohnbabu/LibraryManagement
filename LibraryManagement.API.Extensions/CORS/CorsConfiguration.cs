using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace LibraryManagement.API.Extensions.CORS
{
    public static class CorsConfiguration
    {
        public static IApplicationBuilder UseLMCors(this IApplicationBuilder app, IConfiguration configuration)
        {
            app.UseCors(builder => builder
                   .AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader()
            );

            return app;
        }
    }
}
