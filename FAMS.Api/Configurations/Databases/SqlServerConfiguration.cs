using FAMS.Core.Databases;
using Microsoft.EntityFrameworkCore;

namespace FAMS.Api.Configurations.Databases
{
    public static class SqlServerConfiguration
    {
        public static IServiceCollection ConfigureSqlServer(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<FamsContext>(options =>
           {
               options.UseSqlServer(configuration["ConnectionStrings:DefaultConnection"])
                      .EnableSensitiveDataLogging();
           });
            return services;
        }
    }
}
