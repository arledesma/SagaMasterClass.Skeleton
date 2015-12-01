namespace Shipping.Messages
{
    using NServiceBus;

    public class OrderShipped : IEvent
    {
        public string OrderId { get; set; }
        public string Response { get; set; }
    }
}