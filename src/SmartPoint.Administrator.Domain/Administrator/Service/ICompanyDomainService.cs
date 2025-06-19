using SmartPoint.Administrator.Domain.Administrator.Aggregate;

namespace SmartPoint.Administrator.Domain.Administrator.Service
{
    public interface ICompanyDomainService
    {
        Task UpdateCompanyAsync(Company company, string newName, bool active);
    }
}
