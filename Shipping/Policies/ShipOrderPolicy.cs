namespace Shipping.Policies
{
    using System;
    using Messages;
    using NServiceBus;
    using NServiceBus.Saga;
    
    public class ShipOrderPolicy : Saga<ShipOrderData>, IAmStartedByMessages<ShipOrder>, IHandleMessages<OrderShipped>, IHandleTimeouts<ShipOrderWithFedexTimeout>
    {
        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<ShipOrderData> mapper)
        {
            mapper.ConfigureMapping<ShipOrder>(x => x.OrderId).ToSaga(x => x.OrderId);
            mapper.ConfigureMapping<OrderShipped>(x => x.OrderId).ToSaga(x => x.OrderId);
            mapper.ConfigureMapping<ShipOrderWithFedexTimeout>(x => x.OrderId).ToSaga(x => x.OrderId);
        }

        public void Handle(ShipOrder message)
        {
            Data.OrderId = message.OrderId;
            Data.DateShipStarted = DateTime.UtcNow;
            Bus.Send(new OrderWithFedExShippmentRequested
            {
                OrderId = message.OrderId
            });

            RequestTimeout(TimeSpan.FromSeconds(11), new ShipOrderWithFedexTimeout{OrderId = message.OrderId});
        }

        public void Handle(OrderShipped message)
        {
            Data.DateShipCompleted = DateTime.UtcNow;
            Data.Completed = true;
            MarkAsComplete();
        }

        public void Timeout(ShipOrderWithFedexTimeout message)
        {
            if (Data.Completed.HasValue && Data.Completed.Value)
            {
                return;
            }

            
            Bus.Send(new ShipOrderWithUps{OrderId = message.OrderId});
        }
    }
}