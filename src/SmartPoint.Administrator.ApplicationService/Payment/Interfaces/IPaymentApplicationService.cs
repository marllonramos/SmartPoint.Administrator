using SmartPoint.Administrator.ApplicationService.Payment.DTO;
using SmartPoint.Administrator.ApplicationService.Payment.Requests;
using SmartPoint.Administrator.Domain.Payment.Aggregate;

namespace SmartPoint.Administrator.ApplicationService.Payment.Interfaces
{
    public interface IPaymentApplicationService
    {
        Task<PaymentSmart> CheckoutAsync(CheckoutRequest request);
    }
}
