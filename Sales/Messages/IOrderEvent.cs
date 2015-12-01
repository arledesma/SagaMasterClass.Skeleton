namespace Sales.Messages
{
    using NServiceBus;

    public interface IOrderEvent : IEvent
    {
        string OrderId { get; set; }
    }
}