using SmartPoint.Administrator.ApplicationService.Payment.Requests;

namespace SmartPoint.Administrator.ApplicationService.Payment.Interfaces
{
    public interface IPaymentApplicationService
    {
        Task Checkout(CheckoutRequest request);
    }
}
