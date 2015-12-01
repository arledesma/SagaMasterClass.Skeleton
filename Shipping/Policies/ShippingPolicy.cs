namespace Shipping.Policies
{
    using System;
    using Billing.Contracts;
    using Messages;
    using NServiceBus;
    using NServiceBus.Saga;
    using Sales.Contracts;

    public class ShippingPolicy : Saga<ShippingData>, IAmStartedByMessages<OrderPlaced>, IAmStartedByMessages<OrderBilled>
    {
        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<ShippingData> mapper)
        {
            mapper.ConfigureMapping<OrderPlaced>(x=>x.OrderId).ToSaga(x=>x.OrderId);
            mapper.ConfigureMapping<OrderBilled>(x=>x.OrderId).ToSaga(x=>x.OrderId);
        }

        public void Handle(OrderPlaced message)
        {
            Data.OrderId = message.OrderId;
            Data.DatePlaced = DateTime.UtcNow;
            Data.State |= ShippingStates.Placed;

            if((Data.State & ShippingStates.Billed) == ShippingStates.Billed)
                Bus.Send(new ShipOrder{OrderId = message.OrderId});
        }

        public void Handle(OrderBilled message)
        {
            Data.OrderId = message.OrderId;
            Data.DateBilled = DateTime.UtcNow;
            Data.State |= ShippingStates.Billed;

            if((Data.State & ShippingStates.Placed) == ShippingStates.Placed)
                Bus.Send(new ShipOrder{OrderId = message.OrderId});
        }
    }

    

    public class DummyHandler : IHandleMessages<ShipOrder>
    {
        public void Handle(ShipOrder message)
        {
            Console.WriteLine("Order {0} shipped", message.OrderId);
        }
    }
}
