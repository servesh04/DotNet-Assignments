namespace SmartCourierApp.DeliveryCalculators
{
    public class InternationalDeliveryCalculator
    {
        public double Calculate (double weight) => (weight * 150) + 500;
    }
}