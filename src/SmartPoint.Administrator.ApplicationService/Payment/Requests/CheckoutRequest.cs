namespace SmartPoint.Administrator.ApplicationService.Payment.Requests
{
    public struct CheckoutRequest
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string CompanyName { get; set; }

        public string CardName { get; set; }
        public int CardNumber { get; set; }
        public string ValidadeCard { get; set; }
        public string CVC { get; set; }
        public string CpfCnpjHolder { get; set; }
    }
}
