using System;
using SmartCourierApp.Models;
using SmartCourierApp.DeliveryCalculators;
using SmartCourierApp.Notifications;
using SmartCourierApp.Invoices;
using SmartCourierApp.Services;

namespace SmartCourierApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--- SmartCourierApp Delivery Management System ---");
            Console.Write("Enter Customer Name: ");
            string name = Console.ReadLine() ?? "Guest";
            Console.Write("Enter Email: ");
            string email = Console.ReadLine() ?? "";
            Console.Write("Enter Mobile Number: ");
            string mobile = Console.ReadLine() ?? "";
            Console.Write("Enter Parcel Weight (kg): ");
            double.TryParse(Console.ReadLine(), out double weight);
            Console.Write("Enter Source City: ");
            string source = Console.ReadLine() ?? "";
            Console.Write("Enter destination city: ");
            string destination = Console.ReadLine() ?? "";
            Console.Write("Enter notification type (Email/SMS/Whatsapp): ");
            string notificationType = Console.ReadLine() ?? "Email";

            var booking = new CourierBooking
            {
                Customer = new Customer {Name = name, Email = email, MobileNumber = mobile},
                Parcel = new Parcel {Weight = weight, SourceCity = source, DestinationCity = destination},
                DeliveryType = deliveryType,
                NotificationType = notificationType
            };

            IDeliveryChargeCalculator calculator = deliveryType.ToLower() switch
            {
                "express" => new ExpressDeliveryCalculator(),
                "international" => new InternationalDeliveryCalculator(),
                _ => new StandardDeliveryCalculator()
            };

            INotificationService notificationService = notificationType.ToLower() switch
            {
                "sms" => new SmsNotificationService(),
                "whatsapp" => new WhatsAppNotificationService(),
                _ => new EmailNotificationService()  
            };

            IInvoiceGenerator invoiceGenerator = new ConsoleInvoiceGenerator();

            var bookingService = new CourierBookingService(calculator, notificationService, invoiceGenerator);
            bookingService.ProcessBooking(booking);
            
        }
    }
}