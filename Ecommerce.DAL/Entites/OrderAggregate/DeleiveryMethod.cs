namespace Ecommerce.DAL.Entites.OrderAggregate
{
    public class DeleiveryMethod : BaseEntity<int>
    {
        public string ShortName { get; set; }
        public string DeliveryTime { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
