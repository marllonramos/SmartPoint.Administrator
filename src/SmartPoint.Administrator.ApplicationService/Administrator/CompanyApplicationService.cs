using SmartPoint.Administrator.ApplicationService.Administrator.Interfaces;
using SmartPoint.Administrator.ApplicationService.Administrator.Requests;
using SmartPoint.Administrator.Domain.Administrator.Aggregate;
using SmartPoint.Administrator.Domain.Administrator.Repository;

namespace SmartPoint.Administrator.ApplicationService.Administrator
{
    public class CompanyApplicationService : ICompanyApplicationService
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyApplicationService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<IEnumerable<Company>> GetCompaniesAsync() => await _companyRepository.GetCompaniesAsync();

        public async Task<Company?> GetCompanyByIdAsync(Guid id) => await _companyRepository.GetCompanyByIdAsync(id);

        public async Task CreateAsync(CreateCompanyRequest request)
        {
            var company = new Company(request.Name);

            await _companyRepository.CreateAsync(company);
        }

        public async Task UpdateAsync(UpdateCompanyRequest request)
        {
            var company = await GetCompanyByIdAsync(request.Id);

            if (company == null) throw new Exception("Company not found.");

            company.Update(request.Name, request.Active, request.BlockDate);

            await _companyRepository.UpdateAsync();
        }

        public async Task DeleteAsync(Guid id) => await _companyRepository.DeleteAsync(id);
    }
}
