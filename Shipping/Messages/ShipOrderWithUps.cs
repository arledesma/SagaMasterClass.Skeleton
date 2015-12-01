namespace Shipping.Messages
{
    using NServiceBus;

    public class ShipOrderWithUps : ICommand
    {
        public string OrderId { get; set; }
    }
}