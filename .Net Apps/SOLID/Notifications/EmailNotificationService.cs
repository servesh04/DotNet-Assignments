using System;
namespace SmartCourierApp.Notification
{
    public class EmailNotificationService : INotificationService
    {
        public void SendNotification (string destination, string message)
        {
            Console.WriteLine($"Email sent to {destination} : {message}");
        }
    }
}