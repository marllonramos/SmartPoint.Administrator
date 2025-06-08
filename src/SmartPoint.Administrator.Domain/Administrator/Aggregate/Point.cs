using SmartPoint.Administrator.Domain.Administrator.Enum;
using SmartPoint.Administrator.Domain.Shared;

namespace SmartPoint.Administrator.Domain.Administrator.Aggregate
{
    public class Point : Entity
    {
        internal Point(Guid userId, Guid companyId, PointType type, string? obs, double? latitude, double? longitude, string? location, bool isOvertime, string? reasonOvertime, string? urlPicture, bool isManual, string? reasonManual)
        {
            UserId = userId;
            CompanyId = companyId;
            Type = type;
            var date = DateTime.Now;
            RegisterDate = DateOnly.FromDateTime(date);
            RegisterHours = TimeOnly.FromDateTime(date);
            Obs = obs;
            Latitude = latitude;
            Longitude = longitude;
            Location = location;
            IsOvertime = isOvertime;
            ReasonOvertime = reasonOvertime;
            UrlPicture = urlPicture;
            IsManual = isManual;
            ReasonManual = reasonManual;
        }

        // EF Core
        protected Point() { }

        public Guid UserId { get; private set; }
        public Guid CompanyId { get; private set; }
        public PointType Type { get; private set; }
        public DateOnly RegisterDate { get; private set; }
        public TimeOnly RegisterHours { get; private set; }
        public string? Obs { get; private set; }
        public double? Latitude { get; private set; }
        public double? Longitude { get; private set; }
        public string? Location { get; private set; }
        public bool IsOvertime { get; private set; }
        public string? ReasonOvertime { get; private set; }
        public string? UrlPicture { get; private set; }
        public bool IsManual { get; private set; }
        public string? ReasonManual { get; private set; }

        public void Update(PointType type, string? obs, DateOnly registerDate, TimeOnly registerHours, bool isOvertime, string? reasonOvertime, bool isManual, string? reasonManual)
        {
            Type = type;
            RegisterDate = registerDate;
            RegisterHours = registerHours;
            Obs = obs;
            IsOvertime = isOvertime;
            ReasonOvertime = reasonOvertime;
            UrlPicture = UrlPicture;
            IsManual = isManual;
            ReasonManual = reasonManual;
        }
    }
}
