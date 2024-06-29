using Ecommerce.BLL.Interfaces;
using Ecommerce.BLL.Specifications;
using Ecommerce.DAL.Entites;
using Ecommerce.DAL.Entites.OrderAggregate;

namespace Ecommerce.BLL.Services
{
    public class OrderService : IOrderService
    {
       private readonly IBasketRepository _basketRepo;
        private readonly IUnitOfWork _unitOfWork;
        public OrderService(IBasketRepository basketRepo,
            IUnitOfWork unitOfWork)
        {
            _basketRepo = basketRepo;
            _unitOfWork = unitOfWork;
        }
        public async Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, Address shippingAddress)
        {
            // 1. Get basket from the repo
            var basket = await _basketRepo.GetBasketAsync(basketId);
            // 2. Get items from the product repo
            var items = new List<OrderItem>();
            foreach (var item in basket.Items)
            {
               var productItem = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
               var itemOrdered = new ProductItemOrder (productItem.Id, productItem.Name, productItem.ImageUrl);
               var orderItem = new OrderItem(itemOrdered, productItem.Price, item.Quantity);
               items.Add(orderItem);
            }
            // 3. Get delivery method from repo
            var deliveryMethod = await _unitOfWork.Repository<DeleiveryMethod>().GetByIdAsync(deliveryMethodId);
            // 4. Calc subtotal
            var subtotal = items.Sum(item => item.Price * item.Quantity);

            var spec  = new OrderByPaymentIntentIdSpecification(basket.PaymentIntentId);
            var order = await _unitOfWork.Repository<Order>().GetEntityWithSpec(spec);

            if (order != null)
            {
                order.ShipToAddress = shippingAddress;
                order.DeliveryMethod = deliveryMethod;
                order.Subtotal = subtotal;
                _unitOfWork.Repository<Order>().Update(order);
            }
            else
            {
                 order = new Order(buyerEmail, shippingAddress, deliveryMethod, items, subtotal, basket.PaymentIntentId);
                _unitOfWork.Repository<Order>().Add(order);
            }
            // 5. Create order
            
            // 6. Save to db
            var result = await _unitOfWork.Complete();
            //delete basket
           // await _basketRepo.DeleteBasketAsync(basketId);
            // 7. Return order
            if (result <= 0)
            {
                return null;
            }
            return order;
        }

        public async Task<IReadOnlyList<DeleiveryMethod>> GetDeliveryMethodsAsync()
        {
            return await _unitOfWork.Repository<DeleiveryMethod>().GetAllAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int id, string buyerEmail)
        {
            var spec = new OrderWithItemsAndOrderingSpecicification(id, buyerEmail);
            return await _unitOfWork.Repository<Order>().GetEntityWithSpec(spec);
        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            var spec = new OrderWithItemsAndOrderingSpecicification(buyerEmail);
            return await _unitOfWork.Repository<Order>().ListAsync(spec);
        }
    }
}
