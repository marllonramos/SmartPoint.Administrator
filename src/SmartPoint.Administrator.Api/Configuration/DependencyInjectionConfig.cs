using SmartPoint.Administrator.ApplicationService.Administrator;
using SmartPoint.Administrator.ApplicationService.Administrator.Interfaces;
using SmartPoint.Administrator.ApplicationService.Payment;
using SmartPoint.Administrator.ApplicationService.Payment.Interfaces;
using SmartPoint.Administrator.ApplicationService.Payment.Interfaces.ExternalServices;
using SmartPoint.Administrator.ApplicationService.Shared.Interfaces;
using SmartPoint.Administrator.ApplicationService.Shared.Notifications;
using SmartPoint.Administrator.Domain.Administrator.Repository;
using SmartPoint.Administrator.Domain.Administrator.Service;
using SmartPoint.Administrator.Infra.Administrator.DAO;
using SmartPoint.Administrator.Infra.Administrator.Repository;
using SmartPoint.Administrator.Infra.ExternalServices.Payments;
using SmartPoint.Administrator.Infra.ExternalServices.Payments.MercadoPago;

namespace SmartPoint.Administrator.Api.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void AddDependencyInjection(this IServiceCollection services)
        {
            // Notificator
            services.AddScoped<INotificator, Notificator>();

            // Infra
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IPointRepository, PointRepository>();
            services.AddScoped<IVacationRepository, VacationRepository>();
            services.AddScoped<PaymentMercadoPago, PaymentMercadoPago>();

            // DAO
            services.AddScoped<IReportPointDAO, ReportPointDAO>();
            services.AddScoped<IVacationManagementDAO, VacationManagementDAO>();

            // Service
            services.AddScoped<ICompanyApplicationService, CompanyApplicationService>();
            services.AddScoped<IPointApplicationService, PointApplicationService>();
            services.AddScoped<IVacationApplicationService, VacationApplicationService>();
            services.AddScoped<IPaymentApplicationService, PaymentApplicationService>();
            services.AddScoped<IPaymentExternalServiceAdapter, PaymentExternalServiceAdapter>();

            // DomainService
            services.AddScoped<ICompanyDomainService, CompanyDomainService>();
        }
    }
}
