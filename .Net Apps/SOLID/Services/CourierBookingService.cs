using System;
using SmartCourierApp.DeliveryCalculators;
using SmartCourierApp.Models;
using SmartCourierApp.Notifications;
using SmartCourierApp.Invoices;

namespace SmartCourierApp.Services
{
    public class CourierBookingService
    {
        private readonly IDeliveryChargeCalculator _calculator;
        private readonly INotificationService _notificationService;
        private readonly IInvoiceGenerator _invoiceGenerator;

        public CourierBookingService(IDeliveryChargeCalculator calculator,
        INotificationService notificationService,
        IInvoiceGenerator invoiceGenerator
        )
        {
            _calculator = calculator;
            _notificationService = notificationService;
            _invoiceGenerator = invoiceGenerator;
        }

        public void ProcessBooking(CourierBooking booking)
        {
            booking.TotalCharge = _calculator.Calculate(booking.Parcel.Weight);
            string destination = booking.NotificationType.ToUpper() switch{
                "EMAIL" => booking.Customer.Email,
                "SMS" => booking.Customer.MobileNumber,
                "WHATSAPP" => booking.Customer.MobileNumber,
                _ => booking.Customer.Email    
            };

            string message = $"Your {booking.DeliveryType} booking from {booking.Parcel.SourceCity} to {booking.Parcel.DestinationCity} is confirmed";
            _notificationService.SendNotification(destination, message);
            _invoiceGenerator.GenerateInvoice(booking);
        }

        
    }
}