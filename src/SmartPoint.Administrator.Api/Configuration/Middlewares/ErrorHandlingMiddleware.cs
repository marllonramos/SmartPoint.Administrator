﻿using Newtonsoft.Json;

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
            catch (Exception ex)
            {
                if (!context.Response.HasStarted)
                {
                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "application/json";

                    var errors = new
                    {
                        success = false,
                        result = new
                        {
                            errors = new[] { $"Estamos com problema: {ex.Message}" }
                        }
                    };

                    await context.Response.WriteAsync(JsonConvert.SerializeObject(errors));
                }
            }
        }
    }
}
