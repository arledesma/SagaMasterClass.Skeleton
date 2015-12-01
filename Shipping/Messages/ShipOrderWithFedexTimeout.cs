namespace Shipping.Messages
{
    using NServiceBus;

    public class ShipOrderWithFedexTimeout : IEvent
    {
        public string OrderId { get; set; }
    }
}