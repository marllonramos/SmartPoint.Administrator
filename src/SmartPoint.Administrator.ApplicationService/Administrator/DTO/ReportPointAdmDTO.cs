using SmartPoint.Administrator.Domain.Administrator.Aggregate;

namespace SmartPoint.Administrator.ApplicationService.Administrator.DTO
{
    public struct ReportPointAdmDTO
    {
        public Point Point { get; set; }
        public string CompanyName { get; set; }
        public string UserName { get; set; }
    }
}
