namespace SmartPoint.Administrator.ApplicationService.Payment.DTO
{
    public class PaymentSmartDTO
    {
        public decimal TransactionAmount { get; set; }
        public int Installments { get; set; }
        public string PaymentMethodId { get; set; }
        //public string Token { get; set; }
        public decimal Amount { get; set; }
        public string Email { get; set; }
    }
}
