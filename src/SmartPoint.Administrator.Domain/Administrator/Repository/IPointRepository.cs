using SmartPoint.Administrator.Domain.Administrator.Aggregate;

namespace SmartPoint.Administrator.Domain.Administrator.Repository
{
    public interface IPointRepository
    {
        Task<IEnumerable<Point>> GetPointsAsync();
        Task<Point?> GetPointByIdAsync(Guid id);
        Task CreateAsync(Point point);
        Task UpdateAsync();
        Task DeleteAsync(Guid id);
    }
}
