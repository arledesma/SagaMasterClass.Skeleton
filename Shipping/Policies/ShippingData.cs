namespace Shipping.Policies
{
    using System;
    using NServiceBus.Saga;

    public class ShippingData : ContainSagaData
    {
        [Unique]
        public virtual string OrderId { get; set; }
        public virtual DateTime? DatePlaced { get; set; }
        public virtual DateTime? DateBilled { get; set; }
        public virtual ShippingStates State { get; set; }
    }

    [Flags]
    public enum ShippingStates
    {
        Placed,
        Billed
    }
}