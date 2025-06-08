using SmartPoint.Administrator.Domain.Administrator.Enum;
using SmartPoint.Administrator.Domain.Shared;

namespace SmartPoint.Administrator.Domain.Administrator.Aggregate
{
    public class Vacation : Entity
    {
        internal Vacation(Guid userId, Guid companyId, DateTime startDate, DateTime endDate, string? obs)
        {
            UserId = userId;
            CompanyId = companyId;
            StartDate = startDate;
            EndDate = endDate;
            Obs = obs;
            Status = VacationStatus.Requested;
        }

        // EF Core
        protected Vacation() { }

        public Guid UserId { get; private set; }
        public Guid CompanyId { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public string? Obs { get; private set; }
        public VacationStatus Status { get; private set; }

        public void Update(DateTime startDate, DateTime endDate, string? obs, int status)
        {
            StartDate = startDate;
            EndDate = endDate;
            Obs = obs;
            Status = (VacationStatus)status;
        }
    }
}
