namespace Shipping.Policies
{
    using System;
    using NServiceBus.Saga;

    public class ShipOrderData : ContainSagaData
    {
        public virtual string OrderId { get; set; }
        public virtual DateTime? DateShipStarted { get; set; }
        public virtual DateTime? DateShipCompleted { get; set; }
        public virtual bool? Completed { get; set; }
    }
}