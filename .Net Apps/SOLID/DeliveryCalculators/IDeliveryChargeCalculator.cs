namespace SmartCourierApp.DeliveryCalculators
{
    public interface IDeliveryChargeCalculator
    {
        double Calculate (double weight);
    }
}