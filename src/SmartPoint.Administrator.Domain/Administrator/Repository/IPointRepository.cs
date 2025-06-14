using SmartPoint.Administrator.Domain.Administrator.Aggregate;

namespace SmartPoint.Administrator.Domain.Administrator.Repository
{
    public interface IPointRepository
    {
        Task<IEnumerable<Point>> GetPointsAsync();
        Task<Point?> GetPointByIdAsync(Guid id);
        Task<IEnumerable<Point>?> GetRegistrationHistoryByUserIdAsync(Guid id, DateOnly dateStart, DateOnly dateEnd, TimeOnly? timeStart, TimeOnly? timeEnd);
        Task<IEnumerable<Point>?> GetWeekPointByUserIdAsync(Guid id, DateOnly dateStart, DateOnly dateEnd);
        Task CreateAsync(Point point);
        Task UpdateAsync();
        Task DeleteAsync(Guid id);
    }
}
