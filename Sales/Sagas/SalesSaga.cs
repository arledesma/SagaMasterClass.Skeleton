namespace Sales.Sagas
{
    using System;
    using Messages;
    using NServiceBus;
    using NServiceBus.Saga;

    public class SalesSaga : Saga<SalesSagaData>
        , IAmStartedByMessages<StartOrder>
        , IHandleMessages<PlaceOrder>
        , IHandleMessages<CancelOrder>
        , IHandleTimeouts<OrderTimeout>
    {
        public void Handle(StartOrder message)
        {
            Data.OrderId = message.OrderId;
            this.Bus.Publish(new OrderStarted
            {
                OrderId = Data.OrderId
            });
            RequestTimeout(TimeSpan.FromSeconds(20), new OrderTimeout(Data.OrderId));
        }

        public void Handle(PlaceOrder message)
        {
            Data.IsPlaced = true;
            Bus.Publish(new OrderPlaced{OrderId = message.OrderId});
        }

        public void Handle(CancelOrder message)
        {
            Data.IsCancelled = true;
            Bus.Publish(new OrderCanceled(Data.OrderId));
        }

        public void Timeout(OrderTimeout state)
        {
            if (!Data.IsCancelled && !Data.IsPlaced)
            {
                Bus.Publish(new OrderAbandoned(Data.OrderId));
            }
        }

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<SalesSagaData> mapper)
        {
            mapper.ConfigureMapping<PlaceOrder>(x => x.OrderId).ToSaga(x => x.OrderId);
            mapper.ConfigureMapping<CancelOrder>(x => x.OrderId).ToSaga(x => x.OrderId);
            mapper.ConfigureMapping<OrderTimeout>(x => x.OrderId).ToSaga(x => x.OrderId);
        }
    }
}
