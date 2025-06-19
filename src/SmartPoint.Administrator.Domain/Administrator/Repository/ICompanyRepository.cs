using SmartPoint.Administrator.Domain.Administrator.Aggregate;

namespace SmartPoint.Administrator.Domain.Administrator.Repository
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<Company>> GetCompaniesAsync();
        Task<Company?> GetCompanyByIdAsync(Guid id);
        Task<Company?> GetCompanyByIdOnlyActiveAsync(Guid id);
        Task<Company?> GetCompanyByNameAsync(string name);
        Task CreateAsync(Company company);
        Task UpdateAsync();
        Task DeleteAsync(Company company);
    }
}
