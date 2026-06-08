namespace SmartCourierApp.DeliveryCalculators
{
    public class ExpressDeliveryCalculator : IDeliveryChargeCalculator
    {
        public double Calculate(double weight) => (weight * 80) + 100;
    }
}