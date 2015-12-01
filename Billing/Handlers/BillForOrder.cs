namespace Billing.Handlers
{
    using Contracts;
    using NServiceBus;
    using Sales.Contracts;

    public class BillForOrder : IHandleMessages<OrderPlaced>
    {
        public IBus Bus { get; set; }
        public void Handle(OrderPlaced message)
        {
            Bus.Publish(new OrderBilled{OrderId = message.OrderId});
        }
    }

}
