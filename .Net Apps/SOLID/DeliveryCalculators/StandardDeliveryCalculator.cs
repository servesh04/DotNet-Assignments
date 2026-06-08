namespace SmartCourierApp.DeliveryCalculators
{
    public class StandardDeliveryCalculator : IDeliveryChargeCalculator
    {
        public double Calculate(double weight) => weight * 50;
    }
}