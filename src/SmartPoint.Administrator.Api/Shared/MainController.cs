using Microsoft.AspNetCore.Mvc;
using SmartPoint.Administrator.ApplicationService.Shared.Interfaces;
using SmartPoint.Administrator.ApplicationService.Shared.Notifications;
using System.Net;
using System.Reflection;
using static Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary;

namespace SmartPoint.Administrator.Api.Shared
{
    [ApiController]
    public abstract class MainController : ControllerBase
    {
        private readonly INotificator _notificator;

        protected MainController(INotificator notificator)
        {
            _notificator = notificator;
        }

        protected bool ValidOperation() => !_notificator.HasNotification();

        protected ActionResult CustomResponse(HttpStatusCode statusCode = HttpStatusCode.OK, object result = null!)
        {
            if (ValidOperation())
            {
                return new ObjectResult(new
                {
                    success = true,
                    result
                })
                {
                    StatusCode = (int)statusCode,
                };
            }

            string? methodName = Enum.GetName(typeof(HttpStatusCode), _notificator.GetNotifications().Select(n => n.StatusCode).FirstOrDefault());

            MethodInfo? method = GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public)
                                               .FirstOrDefault(m => m.Name == methodName && m.GetParameters().Length == 1);

            if (methodName is not null)
            {
                return (ObjectResult)method?.Invoke(this, new object[]
                    {
                        new
                        {
                            success = false,
                            result = new
                            {
                                errors = _notificator.GetNotifications().Select(n => n.Message)
                            }
                        }
                    }
                )!;
            }

            return StatusCode((int)HttpStatusCode.InternalServerError, new
            {
                success = false,
                result = new
                {
                    errors = _notificator.GetNotifications().Select(n => n.Message)
                }
            });
        }

        protected void NotifyError(string message, HttpStatusCode statusCode) => _notificator.Handle(new Notification(message, statusCode));

        protected void NotifyError(ValueEnumerable values, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            foreach (var value in values)
            {
                foreach (var error in value.Errors)
                    NotifyError(error.ErrorMessage, statusCode);
            }
        }
    }
}
