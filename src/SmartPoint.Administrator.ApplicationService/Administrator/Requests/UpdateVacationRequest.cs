namespace SmartPoint.Administrator.ApplicationService.Administrator.Requests
{
    public struct UpdateVacationRequest
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid CompanyId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Obs { get; set; }
        public int Status { get; set; }
    }
}
