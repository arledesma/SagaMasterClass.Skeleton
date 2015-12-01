namespace Sales.Messages
{

    public class OrderTimeout : IOrderEvent
    {
        public OrderTimeout() { }
        public OrderTimeout(string orderId)
        {
            OrderId = orderId;
        }

        public string OrderId { get; set; }
    }
}