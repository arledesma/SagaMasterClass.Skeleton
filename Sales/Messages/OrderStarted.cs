namespace Sales.Messages
{
    using NServiceBus;

    public class OrderStarted : IEvent
    {
        public string OrderId { get; set; }
    }
}