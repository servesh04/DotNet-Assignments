namespace SmartCourierApp.models
{
    public class CourierBooking
    {
        public Customer Customer {get;set;} = new();
        public Parcel Parcel {get;set;} = new();
        public string DeliveryType {get;set;} = string.Empty;
        public string NotificationType {get;set;} = string.Empty;
        public double TotalCharge {get;set;}
    }
}