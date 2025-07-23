using Newtonsoft.Json;
using SmartPoint.Administrator.ApplicationService.Payment.DTO;
using SmartPoint.Administrator.Domain.Payment.Aggregate;
using System.Net.Http.Headers;
using System.Text;

namespace SmartPoint.Administrator.Infra.ExternalServices.Payments.MercadoPago
{
    public class PaymentMercadoPago
    {
        public async Task<PaymentSmart> CheckoutAsync(PaymentSmartDTO dto)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "TEST-1592407220474277-071412-6296ec5c0ce915c6c0ea28b313216ed4-99684881");
            client.DefaultRequestHeaders.Add("X-Idempotency-Key", Guid.NewGuid().ToString());

            var payload = new
            {
                transaction_amount = dto.Amount,
                installments = 1,
                payment_method_id = "visa",
                //token = dto.Token,
                description = "Assinatura anual do sistema de ponto",
                payer = new
                {
                    email = dto.Email
                }
            };

            try
            {
                var json = JsonConvert.SerializeObject(payload, Formatting.None,
                                new JsonSerializerSettings
                                {
                                    NullValueHandling = NullValueHandling.Ignore
                                });

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("https://api.mercadopago.com/v1/payments", content);
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Erro MercadoPago: {response.StatusCode} => {result}");
                }

                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
