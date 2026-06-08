using System;

namespace SmartCourierApp.Notifications
{
    public class WhatsAppNotificationService : INotificationService
    {
        public void SendNotification(string destination, string message)
        {
            Console.WriteLine($"Whatsapp sent to {destination} : {message}");
        }
    }
}