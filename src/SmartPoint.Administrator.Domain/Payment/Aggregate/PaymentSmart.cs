namespace SmartPoint.Administrator.Domain.Payment.Aggregate
{
    public class PaymentSmart
    {
        public PaymentSmart(long? id, string status, string statusDetail)
        {
            Id = id;
            Status = status;
            StatusDetail = statusDetail;
        }

        public long? Id { get; private set; }
        public string Status { get; private set; }
        public string StatusDetail { get; private set; }
    }
}
