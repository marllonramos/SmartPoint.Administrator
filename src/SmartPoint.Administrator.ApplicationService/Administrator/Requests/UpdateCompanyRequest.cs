namespace SmartPoint.Administrator.ApplicationService.Administrator.Requests
{
    public struct UpdateCompanyRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public DateTime? BlockDate { get; set; }
    }
}
