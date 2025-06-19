using System.Net;

namespace SmartPoint.Administrator.ApplicationService.Shared.Notifications
{
    public class Notification
    {
        //public Notification(string message, int statusCode)
        //{
        //    Message = message;
        //    StatusCode = statusCode;
        //}

        public Notification(string message, HttpStatusCode statusCode)
        {
            Message = message;
            StatusCode = statusCode;
        }

        public string? Message { get; }
        public HttpStatusCode StatusCode { get; }
    }
}
