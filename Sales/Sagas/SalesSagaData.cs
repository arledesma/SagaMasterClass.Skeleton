namespace Sales.Sagas
{
    using NServiceBus.Saga;

    public class SalesSagaData : ContainSagaData
    {
        [Unique]
        public virtual string OrderId { get; set; }
        public virtual bool IsCancelled { get; set; }
        public virtual bool IsPlaced { get; set; }
    }
}