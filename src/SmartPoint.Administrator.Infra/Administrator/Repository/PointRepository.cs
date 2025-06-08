using Microsoft.EntityFrameworkCore;
using SmartPoint.Administrator.Domain.Administrator.Aggregate;
using SmartPoint.Administrator.Domain.Administrator.Repository;
using SmartPoint.Administrator.Infra.Administrator.Context;

namespace SmartPoint.Administrator.Infra.Administrator.Repository
{
    public class PointRepository : IPointRepository
    {
        private readonly ApplicationDbContext _context;

        public PointRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Point>> GetPointsAsync() => await _context.Points.ToListAsync();

        public async Task<Point?> GetPointByIdAsync(Guid id) => await _context.Points.FirstOrDefaultAsync(p => p.Id == id);

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
