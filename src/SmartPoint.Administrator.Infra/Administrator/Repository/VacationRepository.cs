using Microsoft.EntityFrameworkCore;
using SmartPoint.Administrator.Domain.Administrator.Aggregate;
using SmartPoint.Administrator.Domain.Administrator.Repository;
using SmartPoint.Administrator.Infra.Administrator.Context;

namespace SmartPoint.Administrator.Infra.Administrator.Repository
{
    public class VacationRepository : IVacationRepository
    {
        private readonly ApplicationDbContext _context;

        public VacationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Vacation>> GetVacationsAsync() => await _context.Vacations.ToListAsync();

        public async Task<Vacation?> GetVacationByIdAsync(Guid id) => await _context.Vacations.FirstOrDefaultAsync(v => v.Id == id);

        public async Task CreateAsync(Vacation vacation)
        {
            await _context.Vacations.AddAsync(vacation);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync() => await _context.SaveChangesAsync();

        public async Task DeleteAsync(Guid id)
        {
            var vacation = await GetVacationByIdAsync(id);

            if (vacation != null)
            {
                _context.Vacations.Remove(vacation);
                await _context.SaveChangesAsync();
            }
        }
    }
}
