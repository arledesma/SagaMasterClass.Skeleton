namespace Sales.Messages
{
    public class OrderAbandoned
    {
        public OrderAbandoned(string orderId)
        {
            OrderId = orderId;
        }

        public string OrderId { get; set; }
    }
}