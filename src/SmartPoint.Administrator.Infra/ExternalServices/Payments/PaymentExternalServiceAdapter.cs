using SmartPoint.Administrator.ApplicationService.Payment.DTO;
using SmartPoint.Administrator.ApplicationService.Payment.Interfaces.ExternalServices;
using SmartPoint.Administrator.Domain.Payment.Aggregate;
using SmartPoint.Administrator.Infra.ExternalServices.Payments.MercadoPago;

namespace SmartPoint.Administrator.Infra.ExternalServices.Payments
{
    public class PaymentExternalServiceAdapter : IPaymentExternalServiceAdapter
    {
        private readonly PaymentMercadoPago _paymentMercadoPago;

        public PaymentExternalServiceAdapter(PaymentMercadoPago paymentMercadoPago)
        {
            _paymentMercadoPago = paymentMercadoPago;
        }

        public async Task<PaymentSmart> CheckoutAsync(PaymentSmartDTO dto)
        {
            var payment = await _paymentMercadoPago.CheckoutAsync(dto);

            return payment;
        }
    }
}
