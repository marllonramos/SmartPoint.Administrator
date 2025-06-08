namespace SmartPoint.Administrator.ApplicationService.Administrator.Requests
{
    public struct CreateVacationRequest
    {
        public Guid UserId { get; set; }
        public Guid CompanyId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Obs { get; set; }
    }
}
