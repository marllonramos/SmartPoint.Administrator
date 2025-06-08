using SmartPoint.Administrator.ApplicationService.Administrator.Requests;
using SmartPoint.Administrator.Domain.Administrator.Aggregate;

namespace SmartPoint.Administrator.ApplicationService.Administrator.Interfaces
{
    public interface ICompanyApplicationService
    {
        Task<IEnumerable<Company>> GetCompaniesAsync();
        Task<Company?> GetCompanyByIdAsync(Guid id);
        Task CreateAsync(CreateCompanyRequest request);
        Task UpdateAsync(UpdateCompanyRequest request);
        Task DeleteAsync(Guid id);
    }
}
