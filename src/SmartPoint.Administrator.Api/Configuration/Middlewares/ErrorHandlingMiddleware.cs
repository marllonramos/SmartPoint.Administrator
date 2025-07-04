using Newtonsoft.Json;
using SmartPoint.Administrator.Domain.Administrator.Exceptions;

namespace SmartPoint.Administrator.Api.Configuration.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (BusinessException bex)
            {
                context.Response.StatusCode = 400;
                context.Response.ContentType = "application/json";

                var response = new
                {
                    success = false,
                    result = new
                    {
                        errors = new[] { $"Regra de negócio violada: {bex.Message}" }
                    }
                };

                await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
            }
            catch (Exception ex)
            {
                if (!context.Response.HasStarted)
                {
                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "application/json";

                    var response = new
                    {
                        success = false,
                        result = new
                        {
                            errors = new[] { $"Estamos com problema: {ex.Message}" }
                        }
                    };

                    await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
                }
            }
        }
    }
}
