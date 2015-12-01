namespace FedEx.Gateway.Handlers
{
    using System.Net;
    using NServiceBus.Saga;
    using Shipping.Messages;

    //[EndpointSLA("00:00:10")]
    public class CallFedEx : Saga<CallFedExData>, IAmStartedByMessages<OrderWithFedExShippmentRequested>
    {
        public void Handle(OrderWithFedExShippmentRequested message)
        {
            Data.OrderId = message.OrderId;

            var webClient = new WebClient{ BaseAddress = "http://localhost:8888/"};
            var response = webClient.DownloadString("/fedex/shipit");
            Bus.Publish(new OrderShipped{OrderId = message.OrderId, Response = response});
        }

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<CallFedExData> mapper)
        {
            mapper.ConfigureMapping<OrderWithFedExShippmentRequested>(x=>x.OrderId).ToSaga(x=>x.OrderId);
        }
    }

    public class CallFedExData : ContainSagaData
    {
        public string OrderId { get; set; }
    }
}
