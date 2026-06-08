namespace SmartCourierApp.Models
{
    public class Parcel
    {
        public double Weight {get;set;}
        public string SourceCity {get;set;} = string.Empty;
        public string DestinationCity {get;set;} = string.Empty;
    }
}