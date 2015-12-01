namespace Sales.Messages
{
    public class OrderCanceled
    {
        public OrderCanceled(string orderId)
        {
            OrderId = orderId;
        }

        public string OrderId { get; set; }
    }
}