namespace Ecommerce.DAL.Entites.OrderAggregate
{
    public class Order : BaseEntity<int>
    {
        public Order()
        {

        }
        public Order(string buyerEmail, Address shipToAddress, DeleiveryMethod deliveryMethod, IReadOnlyList<OrderItem> orderItems, decimal subtotal, string paymentIntentId)
        {
            BuyerEmail = buyerEmail;
            ShipToAddress = shipToAddress;
            DeliveryMethod = deliveryMethod;
            OrderItems = orderItems;
            Subtotal = subtotal;
            PaymentIntentId = paymentIntentId;
        }

        public string BuyerEmail { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public Address ShipToAddress { get; set; }
        public DeleiveryMethod DeliveryMethod { get; set; }
        public IReadOnlyList<OrderItem> OrderItems { get; set; }
        public decimal Subtotal { get; set; }
        public OrderStatus MyProperty { get; set; } = OrderStatus.Pending;
        public string PaymentIntentId { get; set; }
        //status of the order
        public object Status { get; set; }
        public decimal GetTotal()
        {
            return Subtotal + DeliveryMethod.Price;
        }
    }
}
