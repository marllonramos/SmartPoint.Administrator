using SmartPoint.Administrator.Domain.Administrator.Aggregate;
using SmartPoint.Administrator.Domain.Administrator.Enum;

namespace SmartPoint.Administrator.Domain.Administrator.Builder
{
    public class VacationBuilder
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public Guid CompanyId { get; private set; }
        public DateOnly StartPeriod { get; private set; }
        public DateOnly EndPeriod { get; private set; }
        public string? Obs { get; private set; }
        public VacationStatus Status { get; private set; }

        public VacationBuilder WithId(Guid id) { Id = id; return this; }
        public VacationBuilder WithUserId(Guid userId) { UserId = userId; return this; }
        public VacationBuilder WithCompanyId(Guid companyId) { CompanyId = companyId; return this; }
        public VacationBuilder WithStartDate(DateOnly startPeriod) { StartPeriod = startPeriod; return this; }
        public VacationBuilder WithEndDate(DateOnly endPeriod) { EndPeriod = endPeriod; return this; }
        public VacationBuilder WithObs(string? obs) { Obs = obs; return this; }
        public VacationBuilder WithEnumStatus(VacationStatus status) { Status = status; return this; }
        public VacationBuilder WithIntStatus(int status) { Status = (VacationStatus)status; return this; }

        public Vacation Build() => new Vacation(UserId, CompanyId, StartPeriod, EndPeriod, Obs);
    }
}
