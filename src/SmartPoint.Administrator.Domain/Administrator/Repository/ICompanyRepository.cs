using SmartPoint.Administrator.Domain.Administrator.Aggregate;

namespace SmartPoint.Administrator.Domain.Administrator.Repository
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<Company>> GetCompaniesAsync();
        Task<Company?> GetCompanyByIdAsync(Guid id);
        Task CreateAsync(Company company);
        Task UpdateAsync();
        Task DeleteAsync(Guid id);
    }
}
