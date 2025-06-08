using SmartPoint.Administrator.ApplicationService.Administrator.Requests;
using SmartPoint.Administrator.Domain.Administrator.Aggregate;

namespace SmartPoint.Administrator.ApplicationService.Administrator.Interfaces
{
    public interface IPointApplicationService
    {
        Task<IEnumerable<Point>> GetPointsAsync();
        Task<Point?> GetPointByIdAsync(Guid id);
        Task CreateAsync(CreatePointRequest request);
        Task UpdateAsync(UpdatePointRequest request);
        Task DeleteAsync(Guid id);
    }
}
