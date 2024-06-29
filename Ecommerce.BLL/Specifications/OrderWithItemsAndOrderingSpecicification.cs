using Ecommerce.DAL.Entites.OrderAggregate;

namespace Ecommerce.BLL.Specifications
{
    public class OrderWithItemsAndOrderingSpecicification : BaseSpecification<Order>
    {
        public OrderWithItemsAndOrderingSpecicification(string email):base(o=>o.BuyerEmail==email)
        {
            AddInclude(o => o.OrderItems);
            AddInclude(o=>o.DeliveryMethod);
            AddOrderByDescending(o=>o.OrderDate);
        }
        public OrderWithItemsAndOrderingSpecicification(int id, string email):base(o => o.Id == id && o.BuyerEmail == email)
        {
            AddInclude(o => o.OrderItems);
            AddInclude(o => o.DeliveryMethod);
        }
    }
}
