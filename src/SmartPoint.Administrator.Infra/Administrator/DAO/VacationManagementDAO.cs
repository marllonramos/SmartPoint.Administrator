using Microsoft.EntityFrameworkCore;
using SmartPoint.Administrator.ApplicationService.Administrator.Interfaces;
using SmartPoint.Administrator.Infra.Administrator.Context;
using SmartPoint.Administrator.Infra.Identity.Context;

namespace SmartPoint.Administrator.Infra.Administrator.DAO
{
    public class VacationManagementDAO : IVacationManagementDAO
    {
        private readonly ApplicationDbContext _context;
        private readonly ApplicationIdentityDbContext _identityContext;

        public VacationManagementDAO(ApplicationDbContext context, ApplicationIdentityDbContext identityContext)
        {
            _context = context;
            _identityContext = identityContext;
        }

        public async Task<IEnumerable<dynamic>?> GetVacationManagementAsync(int startYear, int endYear, Guid? userId)
        {
            try
            {
                var queryVacations = _context.Vacations.Where(v => v.startyear == startYear &&
                                                                v.endyear == endYear
                                                          );

                if (userId != null) queryVacations = queryVacations.Where(p => p.UserId == userId);

                var vacations = await queryVacations.AsNoTracking()
                                              .OrderByDescending(o => o.StartPeriod)
                                              .ToListAsync();

                var userIds = vacations.Select(p => p.UserId).Distinct().ToList();
                var userIdsAsString = userIds.Select(id => id.ToString()).ToList();

                var users = await _identityContext.Users
                                                  .Where(u => userIdsAsString.Contains(u.Id))
                                                  .AsNoTracking()
                                                  .ToListAsync();

                var result = vacations.Join(
                                         users,
                                         vacation => vacation.UserId,
                                         user => Guid.Parse(user.Id),
                                         (vacation, user) => new
                                         {
                                             Vacation = vacation,
                                             user.UserName,
                                             user.Email
                                         })
                                        .ToList();

                return result;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
