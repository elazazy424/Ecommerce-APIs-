using Ecommerce.DAL.Entites;
using Ecommerce.DAL.Entites.OrderAggregate;

namespace Ecommerce.BLL.Interfaces
{
    public interface IPaymentService
    {
        Task<CustomerBasket> createOrUpdatePaymentIntent(string basketId);
        Task<Order> UpdatePaymentSucceeded(string paymentIntentId);
        Task<Order> UpdatePaymentFailed(string paymentIntentId);
    }
}
