namespace SmartPoint.Administrator.ApplicationService.Administrator.Requests
{
    public struct UpdateVacationRequest
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid CompanyId { get; set; }
        public DateOnly StartPeriod { get; set; }
        public DateOnly EndPeriod { get; set; }
        public string? Obs { get; set; }
        public int Status { get; set; }
    }
}
