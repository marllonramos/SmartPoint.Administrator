using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using SmartPoint.Administrator.Api.Shared;
using SmartPoint.Administrator.ApplicationService.Payment.Interfaces;
using SmartPoint.Administrator.ApplicationService.Payment.Requests;
using SmartPoint.Administrator.ApplicationService.Shared.Interfaces;
using System.Net;

namespace SmartPoint.Administrator.Api.Controllers.v1.Payment
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/payment")]
    public class PaymentController : MainController
    {
        private readonly IPaymentApplicationService _paymentApplicationService;

        public PaymentController(INotificator notificator, IPaymentApplicationService paymentApplicationService)
            : base(notificator)
        {
            _paymentApplicationService = paymentApplicationService;
        }

        [HttpPost]
        [Route("go-checkout")]
        public async Task<IActionResult> GoCheckout(CheckoutRequest request)
        {
            if (!ModelState.IsValid)
            {
                NotifyError(ModelState.Values);

                return CustomResponse();
            }

            var result = await _paymentApplicationService.CheckoutAsync(request);

            return CustomResponse(HttpStatusCode.Created, result);
        }
    }
}
