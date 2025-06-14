using SmartPoint.Administrator.Domain.Administrator.Aggregate;
using SmartPoint.Administrator.Domain.Administrator.Enum;

namespace SmartPoint.Administrator.Domain.Administrator.Builder
{
    public class PointBuilder
    {
        public Guid UserId { get; private set; }
        public Guid CompanyId { get; private set; }
        public PointType Type { get; private set; }
        public DateOnly? RegisterDate { get; private set; }
        public TimeOnly? RegisterHours { get; private set; }
        public string? Obs { get; private set; }
        public double? Latitude { get; private set; }
        public double? Longitude { get; private set; }
        public string? Location { get; private set; }
        public bool IsOvertime { get; private set; }
        public string? ReasonOvertime { get; private set; }
        public string? UrlPicture { get; private set; }
        public bool IsManual { get; private set; }
        public string? ReasonManual { get; private set; }

        public PointBuilder WithUserId(Guid userId) { UserId = userId; return this; }
        public PointBuilder WithCompanyId(Guid companyId) { CompanyId = companyId; return this; }
        public PointBuilder WithType(int type) { Type = (PointType)type; return this; }
        public PointBuilder WithRegisterDate(DateOnly? registerDate)
        {
            RegisterDate = registerDate;
            return this;
        }
        public PointBuilder WithRegisterHours(TimeOnly? registerHours)
        {
            RegisterHours = registerHours;
            return this;
        }
        public PointBuilder WithObs(string? obs) { Obs = obs; return this; }
        public PointBuilder WithLatitude(double? latitude) { Latitude = latitude; return this; }
        public PointBuilder WithLongitude(double? longitude) { Longitude = longitude; return this; }
        public PointBuilder WithLocation(string? location) { Location = location; return this; }
        public PointBuilder WithOvertime(bool isOvertime) { IsOvertime = isOvertime; return this; }
        public PointBuilder WithReasonOvertime(string? reasonOvertime) { ReasonOvertime = reasonOvertime; return this; }
        public PointBuilder WithUrlPicture(string? urlPicture) { UrlPicture = urlPicture; return this; }
        public PointBuilder WithManual(bool isManual) { IsManual = isManual; return this; }
        public PointBuilder WithReasonManual(string? reasonManual) { ReasonManual = reasonManual; return this; }

        public Point Build()
        {
            if (RegisterDate != null && RegisterHours != null)
                return new Point(UserId, CompanyId, Type, RegisterDate, RegisterHours, Obs, Latitude, Longitude, Location, IsOvertime, ReasonOvertime, UrlPicture, IsManual, ReasonManual);

            return new Point(UserId, CompanyId, Type, null, null, Obs, Latitude, Longitude, Location, IsOvertime, ReasonOvertime, UrlPicture, IsManual, ReasonManual);
        }
    }
}
