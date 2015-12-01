namespace Shipping.Messages
{
    using NServiceBus;

    public class OrderWithFedExShippmentRequested : ICommand
    {
        public string OrderId { get; set; }
    }
}