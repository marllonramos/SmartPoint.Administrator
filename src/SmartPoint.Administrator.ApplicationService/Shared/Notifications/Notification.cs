namespace SmartPoint.Administrator.ApplicationService.Shared.Notifications
{
    public class Notification
    {
        public Notification(string message, int statusCode)
        {
            Message = message;
            StatusCode = statusCode;
        }

        public string? Message { get; }
        public int StatusCode { get; }
    }
}
