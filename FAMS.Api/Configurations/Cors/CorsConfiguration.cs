using FAMS.Core.Databases;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace FAMS.Api.Configurations.Cors
{
    public static class CorsConfiguration
    {
        public static void ConfigureCors(this WebApplication app,
            IConfiguration configuration)
        {
            var corsOrigins = configuration.GetSection("Cors").Get<string[]>();
            if (corsOrigins == null || corsOrigins.Length == 0)
            {
                throw new InvalidOperationException("CORS origins are not provided or are invalid.");
            }

 
            foreach (var origin in corsOrigins)
            {
                Console.WriteLine($"Allowed CORS Origin: {origin}");
            }

            app.UseCors(builder => builder.WithOrigins(corsOrigins)
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials());
        }
    }
}
