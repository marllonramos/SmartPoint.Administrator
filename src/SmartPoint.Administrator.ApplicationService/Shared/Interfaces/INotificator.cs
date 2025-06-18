using SmartPoint.Administrator.ApplicationService.Shared.Notifications;

namespace SmartPoint.Administrator.ApplicationService.Shared.Interfaces
{
    public interface INotificator
    {
        void Handle(Notification notification);
        List<Notification> GetNotifications();
        bool HasNotification();
    }
}
