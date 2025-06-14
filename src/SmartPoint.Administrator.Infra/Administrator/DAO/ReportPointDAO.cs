using Microsoft.EntityFrameworkCore;
using SmartPoint.Administrator.ApplicationService.Administrator.DTO;
using SmartPoint.Administrator.ApplicationService.Administrator.Interfaces;
using SmartPoint.Administrator.Infra.Administrator.Context;
using SmartPoint.Administrator.Infra.Identity.Context;

namespace SmartPoint.Administrator.Infra.Administrator.DAO
{
    public class ReportPointDAO : IReportPointDAO
    {
        private readonly ApplicationDbContext _context;
        private readonly ApplicationIdentityDbContext _identityContext;

        public ReportPointDAO(ApplicationDbContext context, ApplicationIdentityDbContext identityContext)
        {
            _context = context;
            _identityContext = identityContext;
        }

        public async Task<IEnumerable<ReportPointAdmDTO>?> GetReportPointByCompanyAndUserId(Guid id, Guid userId, DateOnly dateStart, DateOnly dateEnd, TimeOnly timeStart, TimeOnly timeEnd)
        {
            var user = await _identityContext.Users.FirstOrDefaultAsync(u => u.Id == userId.ToString());

            if (user == null) return Enumerable.Empty<ReportPointAdmDTO>();

            var pointsAndCompany = await _context.Points
                                    .Where(p => p.CompanyId == id &&
                                                p.UserId == userId &&
                                                p.RegisterDate >= dateStart &&
                                                p.RegisterDate <= dateEnd &&
                                                p.RegisterHours >= timeStart &&
                                                p.RegisterHours <= timeEnd)
                                    .Join(_context.Companies,
                                                point => point.CompanyId,
                                                company => company.Id,
                                                (point, company) => new { point, company })
                                    .ToListAsync();

            var result = pointsAndCompany
                            .Select(pc => new ReportPointAdmDTO
                            {
                                Point = pc.point,
                                CompanyName = pc?.company?.Name!,
                                UserName = user.UserName!
                            })
                            .ToList();

            return result;
        }
    }
}
