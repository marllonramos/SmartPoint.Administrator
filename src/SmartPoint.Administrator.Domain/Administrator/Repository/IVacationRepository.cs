using SmartPoint.Administrator.Domain.Administrator.Aggregate;

namespace SmartPoint.Administrator.Domain.Administrator.Repository
{
    public interface IVacationRepository
    {
        Task<IEnumerable<Vacation>> GetVacationsAsync();
        Task<Vacation?> GetVacationByIdAsync(Guid id);
        Task<IEnumerable<Vacation>?> GetVacationByUserIdAsync(Guid userId, int startYear, int endYear);
        Task CreateAsync(Vacation vacation);
        Task UpdateAsync();
        Task DeleteAsync(Vacation vacation);
    }
}
