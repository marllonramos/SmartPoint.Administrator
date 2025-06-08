using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SmartPoint.Administrator.Infra.Identity.Context;
using SmartPoint.Administrator.Infra.Identity.Entity;

namespace SmartPoint.Administrator.Api.Configuration
{
    public static class IdentityConfig
    {
        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationIdentityDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("IdentityConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationIdentityDbContext>()
                .AddDefaultTokenProviders();

            return services;
        }
    }
}
