using SmartPoint.Administrator.ApplicationService.Administrator.Interfaces;
using SmartPoint.Administrator.ApplicationService.Administrator.Requests;
using SmartPoint.Administrator.ApplicationService.Shared.Interfaces;
using SmartPoint.Administrator.ApplicationService.Shared.Notifications;
using SmartPoint.Administrator.Domain.Administrator.Aggregate;
using SmartPoint.Administrator.Domain.Administrator.Repository;
using SmartPoint.Administrator.Domain.Administrator.Service;
using System.Net;

namespace SmartPoint.Administrator.ApplicationService.Administrator
{
    public class CompanyApplicationService : ICompanyApplicationService
    {
        private readonly INotificator _notificator;
        private readonly ICompanyRepository _companyRepository;
        private readonly ICompanyDomainService _companyDomainService;

        public CompanyApplicationService(INotificator notificator, ICompanyRepository companyRepository, ICompanyDomainService companyDomainService)
        {
            _notificator = notificator;
            _companyRepository = companyRepository;
            _companyDomainService = companyDomainService;
        }

        public async Task<IEnumerable<Company>?> GetCompaniesAsync()
        {
            var companies = await _companyRepository.GetCompaniesAsync();

            if (companies == null || companies.Count() == 0)
            {
                _notificator.Handle(new Notification("Não foram encontradas nenhuma empresa ou estão inativas.", HttpStatusCode.NotFound));

                return default;
            }

            return companies;
        }

        public async Task<Company?> GetCompanyByIdAsync(Guid id)
        {
            var company = await _companyRepository.GetCompanyByIdAsync(id);

            if (company == null)
            {
                _notificator.Handle(new Notification("Empresa não existe.", HttpStatusCode.NotFound));

                return default;
            }

            return company;
        }

        public async Task<Company?> GetCompanyByIdOnlyActiveAsync(Guid id)
        {
            var company = await _companyRepository.GetCompanyByIdOnlyActiveAsync(id);

            if (company == null)
            {
                _notificator.Handle(new Notification("Empresa não existe ou está inativa.", HttpStatusCode.NotFound));

                return default;
            }

            return company;
        }

        public async Task<Company?> GetCompanyByNameAsync(string name) => await _companyRepository.GetCompanyByNameAsync(name);

        public async Task CreateAsync(CreateCompanyRequest request)
        {
            var company = await _companyRepository.GetCompanyByNameAsync(request.Name);

            if (company != null)
            {
                _notificator.Handle(new Notification($"A empresa {request.Name} já existe.", HttpStatusCode.Conflict));

                return;
            }

            var newCompany = new Company(request.Name);

            await _companyRepository.CreateAsync(newCompany);
        }

        public async Task UpdateAsync(UpdateCompanyRequest request)
        {
            var company = await GetCompanyByIdAsync(request.Id);

            if (company == null) return;

            await _companyDomainService.UpdateCompanyAsync(company, request.Name, request.Active);

            await _companyRepository.UpdateAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var company = await GetCompanyByIdAsync(id);

            if (company == null) return;

            await _companyRepository.DeleteAsync(company);
        }
    }
}
