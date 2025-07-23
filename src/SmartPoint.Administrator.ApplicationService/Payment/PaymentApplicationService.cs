using SmartPoint.Administrator.ApplicationService.Payment.DTO;
using SmartPoint.Administrator.ApplicationService.Payment.Interfaces;
using SmartPoint.Administrator.ApplicationService.Payment.Interfaces.ExternalServices;
using SmartPoint.Administrator.ApplicationService.Payment.Requests;
using SmartPoint.Administrator.Domain.Payment.Aggregate;

namespace SmartPoint.Administrator.ApplicationService.Payment
{
    public class PaymentApplicationService : IPaymentApplicationService
    {
        private readonly IPaymentExternalServiceAdapter _paymentExternalServiceAdapter;

        public PaymentApplicationService(IPaymentExternalServiceAdapter paymentExternalServiceAdapter)
        {
            _paymentExternalServiceAdapter = paymentExternalServiceAdapter;
        }

        public async Task<PaymentSmart> CheckoutAsync(CheckoutRequest request)
        {
            var dto = new PaymentSmartDTO
            {
                Amount = request.TransactionAmount,
                Email = request.Email,
                //Token = request.Token
            };

            var payment = await _paymentExternalServiceAdapter.CheckoutAsync(dto);

            return payment;
        }
    }
}
