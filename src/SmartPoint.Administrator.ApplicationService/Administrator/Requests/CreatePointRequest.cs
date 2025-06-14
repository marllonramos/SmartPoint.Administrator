namespace SmartPoint.Administrator.ApplicationService.Administrator.Requests
{
    public struct CreatePointRequest
    {
        public Guid UserId { get; set; }
        public Guid CompanyId { get; set; }
        public int Type { get; set; }
        public DateOnly? RegisterDate { get; set; }
        public TimeOnly? RegisterHours { get; set; }
        public string? Obs { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string? Location { get; set; }
        public bool IsOvertime { get; set; }
        public string? ReasonOvertime { get; set; }
        public string? UrlPicture { get; set; }
        public bool IsManual { get; set; }
        public string? ReasonManual { get; set; }
    }
}
