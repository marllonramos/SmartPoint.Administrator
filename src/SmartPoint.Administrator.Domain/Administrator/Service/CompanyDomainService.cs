using SmartPoint.Administrator.Domain.Administrator.Aggregate;
using SmartPoint.Administrator.Domain.Administrator.Exceptions;
using SmartPoint.Administrator.Domain.Administrator.Repository;

namespace SmartPoint.Administrator.Domain.Administrator.Service
{
    public class CompanyDomainService : ICompanyDomainService
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyDomainService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task UpdateCompanyAsync(Company company, string newName, bool active)
        {
            if (company.Name != newName.Trim())
            {
                var companyByName = await _companyRepository.GetCompanyByNameAsync(newName);

                if (companyByName != null)
                    throw new BusinessException("Já existe uma empresa com este nome.");
            }

            company.Update(newName, active);
        }
    }
}
