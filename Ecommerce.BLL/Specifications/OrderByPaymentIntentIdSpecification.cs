using Ecommerce.DAL.Entites.OrderAggregate;

namespace Ecommerce.BLL.Specifications
{
    public class OrderByPaymentIntentIdSpecification : BaseSpecification<Order>
    {
        public OrderByPaymentIntentIdSpecification(string paymentIntentId) : base(o => o.PaymentIntentId == paymentIntentId)
        {
        }
    }
}
