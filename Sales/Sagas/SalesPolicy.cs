namespace Sales.Sagas
{
    using System;
    using Contracts;
    using Messages;
    using NServiceBus;
    using NServiceBus.Saga;

    public class SalesPolicy : Saga<SalesSagaData>
        , IAmStartedByMessages<StartOrder>
        , IHandleMessages<PlaceOrder>
        , IHandleMessages<CancelOrder>
        , IHandleTimeouts<OrderTimeout>
    {
        public void Handle(StartOrder message)
        {
            Data.OrderId = message.OrderId;
            Data.State = OrderState.Tentative;
            Bus.Publish(new OrderStarted
            {
                OrderId = Data.OrderId
            });
            RequestTimeout(TimeSpan.FromSeconds(20), new OrderTimeout(Data.OrderId));
        }

        public void Handle(PlaceOrder message)
        {
            Data.State = OrderState.Placed;
            Bus.Publish(new OrderPlaced{OrderId = message.OrderId});
        }

        public void Handle(CancelOrder message)
        {
            Data.State = OrderState.Canceled;
            Bus.Publish(new OrderCanceled(Data.OrderId));
        }

        public void Timeout(OrderTimeout state)
        {
            if (Data.State != OrderState.Tentative)
            {
                return;
            }

            Bus.Publish(new OrderAbandoned(Data.OrderId));
            MarkAsComplete();
        }

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<SalesSagaData> mapper)
        {
            mapper.ConfigureMapping<PlaceOrder>(x => x.OrderId).ToSaga(x => x.OrderId);
            mapper.ConfigureMapping<CancelOrder>(x => x.OrderId).ToSaga(x => x.OrderId);
            mapper.ConfigureMapping<OrderTimeout>(x => x.OrderId).ToSaga(x => x.OrderId);
        }
    }
}
