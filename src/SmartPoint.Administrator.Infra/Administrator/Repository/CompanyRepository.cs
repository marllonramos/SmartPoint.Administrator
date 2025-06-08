using Microsoft.EntityFrameworkCore;
using SmartPoint.Administrator.Domain.Administrator.Aggregate;
using SmartPoint.Administrator.Domain.Administrator.Repository;
using SmartPoint.Administrator.Infra.Administrator.Context;

namespace SmartPoint.Administrator.Infra.Administrator.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly ApplicationDbContext _context;

        public CompanyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Company>> GetCompaniesAsync() => await _context.Companies.ToListAsync();

        public async Task<Company?> GetCompanyByIdAsync(Guid id) => await _context.Companies.FirstOrDefaultAsync(c => c.Id == id);

        public async Task CreateAsync(Company company)
        {
            await _context.Companies.AddAsync(company);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync() => await _context.SaveChangesAsync();

        public async Task DeleteAsync(Guid id)
        {
            var company = await GetCompanyByIdAsync(id);

            if (company != null)
            {
                _context.Companies.Remove(company);
                await _context.SaveChangesAsync();
            }
        }
    }
}
