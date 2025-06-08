using Microsoft.EntityFrameworkCore;
using SmartPoint.Administrator.Infra.Administrator.Context;

namespace SmartPoint.Administrator.Api.Configuration
{
    public static class DBConfig
    {
        public static IServiceCollection AddDBConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }
    }
}
