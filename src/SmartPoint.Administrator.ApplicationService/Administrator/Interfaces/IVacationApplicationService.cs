using SmartPoint.Administrator.ApplicationService.Administrator.Requests;
using SmartPoint.Administrator.Domain.Administrator.Aggregate;

namespace SmartPoint.Administrator.ApplicationService.Administrator.Interfaces
{
    public interface IVacationApplicationService
    {
        Task<IEnumerable<Vacation>?> GetVacationsAsync();
        Task<Vacation?> GetVacationByIdAsync(Guid id);
        Task<IEnumerable<Vacation>?> GetVacationByUserIdAsync(Guid userId, int startYear, int endYear);
        Task<IEnumerable<dynamic>?> GetVacationsManagementAsync(int startYear, int endYear, Guid? userId);
        Task CancellateVacationAsync(Guid id);
        Task CreateAsync(CreateVacationRequest request);
        Task UpdateAsync(UpdateVacationRequest request);
        Task DeleteAsync(Guid id);
    }
}
