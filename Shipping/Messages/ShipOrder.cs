namespace Shipping.Messages
{
    using Sales.Messages;

    public class ShipOrder : IOrderCommand
    {
        public string OrderId { get; set; }
    }
}