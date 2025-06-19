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

        public async Task<IEnumerable<Company>> GetCompaniesAsync() => await _context.Companies.Where(c => c.Active == true).AsNoTracking().ToListAsync();

        public async Task<Company?> GetCompanyByIdAsync(Guid id) => await _context.Companies.FirstOrDefaultAsync(c => c.Id == id);

        public async Task<Company?> GetCompanyByIdOnlyActiveAsync(Guid id) => await _context.Companies.FirstOrDefaultAsync(c => c.Id == id && c.Active == true);

        public async Task<Company?> GetCompanyByNameAsync(string name) => await _context.Companies.Where(c => EF.Functions.ILike(c.Name, name.Trim())).AsNoTracking().FirstOrDefaultAsync();

        public async Task CreateAsync(Company company)
        {
            await _context.Companies.AddAsync(company);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync() => await _context.SaveChangesAsync();

        public async Task DeleteAsync(Company company)
        {
            if (company != null)
            {
                _context.Companies.Remove(company);
                await _context.SaveChangesAsync();
            }
        }
    }
}
