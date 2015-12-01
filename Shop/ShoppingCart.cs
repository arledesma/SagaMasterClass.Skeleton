namespace Shop
{
    class ShoppingCart
    {
        public ShoppingCart(string orderId)
        {
            OrderId = orderId;
        }

        public string OrderId { get; private set; }

        public class OrderItem
        {
        }
    }
}