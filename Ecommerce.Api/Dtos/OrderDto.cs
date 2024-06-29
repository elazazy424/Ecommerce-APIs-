using Ecommerce.DAL.Entites.OrderAggregate;

namespace Ecommerce.Api.Dtos
{
    public class OrderDto
    {
        public string BasketId { get; set; }
        public AddressDto ShippingAddress { get; set; }
        public int DeliveryMethodId { get; set; }
    }
}
