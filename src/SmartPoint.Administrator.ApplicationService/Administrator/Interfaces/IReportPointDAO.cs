using SmartPoint.Administrator.ApplicationService.Administrator.DTO;

namespace SmartPoint.Administrator.ApplicationService.Administrator.Interfaces
{
    public interface IReportPointDAO
    {
        Task<IEnumerable<ReportPointAdmDTO>?> GetReportPointByCompanyAndUserId(Guid id, Guid userId, DateOnly dateStart, DateOnly dateEnd, TimeOnly timeStart, TimeOnly timeEnd);
    }
}
