namespace SmartCourierApp.Notifications
{
    public interface INotificationService
    {
        void SendNotification(string destination,string message);
    }
}