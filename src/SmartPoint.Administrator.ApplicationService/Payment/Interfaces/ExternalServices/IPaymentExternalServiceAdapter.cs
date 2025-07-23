using SmartPoint.Administrator.ApplicationService.Payment.DTO;
using SmartPoint.Administrator.Domain.Payment.Aggregate;

namespace SmartPoint.Administrator.ApplicationService.Payment.Interfaces.ExternalServices
{
    public interface IPaymentExternalServiceAdapter
    {
        Task<PaymentSmart> CheckoutAsync(PaymentSmartDTO dto);
    }
}
