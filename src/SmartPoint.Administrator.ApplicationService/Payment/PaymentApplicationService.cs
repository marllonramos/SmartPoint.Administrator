using SmartPoint.Administrator.ApplicationService.Payment.Interfaces;
using SmartPoint.Administrator.ApplicationService.Payment.Requests;

namespace SmartPoint.Administrator.ApplicationService.Payment
{
    public class PaymentApplicationService : IPaymentApplicationService
    {
        public Task Checkout(CheckoutRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
