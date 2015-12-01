namespace Sales.Messages
{
    using NServiceBus;

    public class OrderAbandoned : IEvent
    {
        public OrderAbandoned() { }
        public OrderAbandoned(string orderId)
        {
            OrderId = orderId;
        }

        public string OrderId { get; set; }
    }
}