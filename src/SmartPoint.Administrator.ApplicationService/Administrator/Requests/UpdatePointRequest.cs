using SmartPoint.Administrator.Domain.Administrator.Enum;

namespace SmartPoint.Administrator.ApplicationService.Administrator.Requests
{
    public struct UpdatePointRequest
    {
        public Guid Id { get; set; }
        public PointType Type { get; set; }
        public DateOnly RegisterDate { get; set; }
        public TimeOnly RegisterHours { get; set; }
        public string? Obs { get; set; }
        public bool IsOvertime { get; set; }
        public string? ReasonOvertime { get; set; }
        public bool IsManual { get; set; }
        public string? ReasonManual { get; set; }
    }
}
