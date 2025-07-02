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
            CreatedAt = DateTime.UtcNow;
            ModifiedAt = null;
            StartYear = StartPeriod.Year;
            EndYear = EndPeriod.Year;
        }

        // EF Core
        protected Vacation() { }

        public Guid UserId { get; private set; }
        public Guid CompanyId { get; private set; }
        public DateOnly StartPeriod { get; private set; }
        public DateOnly EndPeriod { get; private set; }
        public string? Obs { get; private set; }
        public VacationStatus Status { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? ModifiedAt { get; private set; }
        public int StartYear { get; private set; }
        public int EndYear { get; private set; }

        public void Update(DateOnly startDate, DateOnly endDate, string? obs, int status)
        {
            StartPeriod = startDate;
            EndPeriod = endDate;
            Obs = obs;
            Status = (VacationStatus)status;
            ModifiedAt = DateTime.UtcNow;
            StartYear = StartPeriod.Year;
            EndYear = EndPeriod.Year;
        }

        public void Approve()
        {
            ModifiedAt = DateTime.UtcNow;
            Status = VacationStatus.Approved;
        }

        public void Reject()
        {
            ModifiedAt = DateTime.UtcNow;
            Status = VacationStatus.Rejected;
        }

        public void Cancel()
        {
            ModifiedAt = DateTime.UtcNow;
            Status = VacationStatus.Cancelled;
        }
    }
}
