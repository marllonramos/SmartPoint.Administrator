using Microsoft.EntityFrameworkCore;
using SmartPoint.Administrator.Domain.Administrator.Aggregate;
using SmartPoint.Administrator.Domain.Administrator.Repository;
using SmartPoint.Administrator.Infra.Administrator.Context;
using SmartPoint.Administrator.Infra.Identity.Context;

namespace SmartPoint.Administrator.Infra.Administrator.Repository
{
    public class PointRepository : IPointRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ApplicationIdentityDbContext _identityContext;

        public PointRepository(ApplicationDbContext context, ApplicationIdentityDbContext identityContext)
        {
            _context = context;
            _identityContext = identityContext;
        }

        public async Task<IEnumerable<Point>> GetPointsAsync() => await _context.Points.ToListAsync();

        public async Task<Point?> GetPointByIdAsync(Guid id) => await _context.Points.FirstOrDefaultAsync(p => p.Id == id);

        public async Task<IEnumerable<Point>?> GetRegistrationHistoryByUserIdAsync(Guid id, DateOnly dateStart, DateOnly dateEnd, TimeOnly? timeStart, TimeOnly? timeEnd)
        {
            var timeS = timeStart == null ? TimeOnly.MinValue : timeStart;
            var timeE = timeEnd == null ? TimeOnly.MaxValue : timeEnd;

            return await _context.Points.Where(p => p.UserId == id &&
                                                    p.RegisterDate >= dateStart &&
                                                    p.RegisterDate <= dateEnd &&
                                                    p.RegisterHours >= timeS &&
                                                    p.RegisterHours <= timeE
                                              )
                                              .AsNoTracking()
                                              .OrderByDescending(o => o.RegisterHours)
                                              .ToListAsync();
        }

        public async Task<IEnumerable<Point>?> GetWeekPointByUserIdAsync(Guid id, DateOnly dateStart, DateOnly dateEnd)
        {
            return await _context.Points.Where(p => p.UserId == id &&
                                                    p.RegisterDate >= dateStart &&
                                                    p.RegisterDate <= dateEnd
                                              )
                                              .AsNoTracking()
                                              .OrderByDescending(o => o.RegisterDate)
                                              .ThenByDescending(o => o.RegisterHours)
                                              .ToListAsync();
        }

        public async Task CreateAsync(Point point)
        {
            await _context.Points.AddAsync(point);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync() => await _context.SaveChangesAsync();

        public async Task DeleteAsync(Guid id)
        {
            var point = await GetPointByIdAsync(id);

            if (point != null)
            {
                _context.Points.Remove(point);
                await _context.SaveChangesAsync();
            }
        }
    }
}
