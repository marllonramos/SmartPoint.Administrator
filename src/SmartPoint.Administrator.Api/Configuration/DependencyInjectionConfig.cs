using SmartPoint.Administrator.ApplicationService.Administrator;
using SmartPoint.Administrator.ApplicationService.Administrator.Interfaces;
using SmartPoint.Administrator.Domain.Administrator.Repository;
using SmartPoint.Administrator.Infra.Administrator.DAO;
using SmartPoint.Administrator.Infra.Administrator.Repository;

namespace SmartPoint.Administrator.Api.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void AddDependencyInjection(this IServiceCollection services)
        {
            // Infra
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IPointRepository, PointRepository>();
            services.AddScoped<IVacationRepository, VacationRepository>();

            // DAO
            services.AddScoped<IReportPointDAO, ReportPointDAO>();

            // Service
            services.AddScoped<ICompanyApplicationService, CompanyApplicationService>();
            services.AddScoped<IPointApplicationService, PointApplicationService>();
            services.AddScoped<IVacationApplicationService, VacationApplicationService>();
        }
    }
}
