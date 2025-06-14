using SmartPoint.Administrator.Domain.Administrator.Enum;
using SmartPoint.Administrator.Domain.Shared;

namespace SmartPoint.Administrator.Domain.Administrator.Aggregate
{
    public class Vacation : Entity
    {
        internal Vacation(Guid userId, Guid companyId, DateOnly startPeriod, DateOnly endPeriod, string? obs)
        {
            UserId = userId;
            CompanyId = companyId;
            StartPeriod = startPeriod;
            EndPeriod = endPeriod;
            Obs = obs;
            Status = VacationStatus.Requested;
            createdat = DateTime.UtcNow;
            modifiedat = null;
            startyear = StartPeriod.Year;
            endyear = EndPeriod.Year;
        }

        // EF Core
        protected Vacation() { }

        public Guid UserId { get; private set; }
        public Guid CompanyId { get; private set; }
        public DateOnly StartPeriod { get; private set; }
        public DateOnly EndPeriod { get; private set; }
        public string? Obs { get; private set; }
        public VacationStatus Status { get; private set; }
        public DateTime createdat { get; private set; }
        public DateTime? modifiedat { get; private set; }
        public int startyear { get; private set; }
        public int endyear { get; private set; }

        public void Update(DateOnly startDate, DateOnly endDate, string? obs, int status)
        {
            StartPeriod = startDate;
            EndPeriod = endDate;
            Obs = obs;
            Status = (VacationStatus)status;
            modifiedat = DateTime.UtcNow;
            startyear = StartPeriod.Year;
            endyear = EndPeriod.Year;
        }

        public void Approve()
        {
            modifiedat = DateTime.UtcNow;
            Status = VacationStatus.Approved;
        }

        public void Reject()
        {
            modifiedat = DateTime.UtcNow;
            Status = VacationStatus.Rejected;
        }

        public void Cancel()
        {
            modifiedat = DateTime.UtcNow;
            Status = VacationStatus.Cancelled;
        }
    }
}
