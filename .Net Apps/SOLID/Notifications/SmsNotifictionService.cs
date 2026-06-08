using System;
namespace SmartCourierApp.Notifications
{
    public class SmsNotificationService : INotificationService
    {
        public void SendNotificationService (string destination, string message){
            Console.WriteLine($"SMS sent to {destination} : {message}");
        }
    }
}