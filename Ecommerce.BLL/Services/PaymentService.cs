﻿using Ecommerce.BLL.Interfaces;
using Ecommerce.BLL.Specifications;
using Ecommerce.DAL.Entites;
using Ecommerce.DAL.Entites.OrderAggregate;
using Microsoft.Extensions.Configuration;
using Stripe;
using Product = Ecommerce.DAL.Entites.Product;
namespace Ecommerce.BLL.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IBasketRepository _basketRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _config;

        public PaymentService(IBasketRepository basketRepository, IUnitOfWork unitOfWork , IConfiguration config)
        {
            _basketRepo = basketRepository;
            _unitOfWork = unitOfWork;
            _config = config;
        }
        public async Task<CustomerBasket> createOrUpdatePaymentIntent(string basketId)
        {
            StripeConfiguration.ApiKey = _config["StripeSettings:SecretKey"];
            var basket = await _basketRepo.GetBasketAsync(basketId);
            if (basket == null)
            {
                return null;
            }

            var shippingPrice = 0m;
            if (basket.DeliveryMethodId.HasValue)
            {
                var deleiveryMethod = await _unitOfWork.Repository<DeleiveryMethod>()
                    .GetByIdAsync((int)basket.DeliveryMethodId);
                shippingPrice = deleiveryMethod.Price;
            }
            foreach (var item in basket.Items)
            {
                var productItem = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                if (item.Price != productItem.Price)
                {
                    item.Price = productItem.Price;
                }
            }
            var service = new PaymentIntentService();
            PaymentIntent intent;
            if (string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = (long)basket.Items.Sum(i => i.Quantity * (i.Price * 100)) + (long)shippingPrice * 100,
                    Currency = "usd",
                    PaymentMethodTypes = new List<string> { "card" }
                };
                intent = await service.CreateAsync(options);
                basket.PaymentIntentId = intent.Id;
                basket.ClientSecret = intent.ClientSecret;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = (long)basket.Items.Sum(i => i.Quantity * (i.Price * 100)) + (long)shippingPrice * 100
                };
                await service.UpdateAsync(basket.PaymentIntentId, options);
            }
            await _basketRepo.UpdateBasketAsync(basket);
            return basket;
        }

        public async Task<Order> UpdatePaymentFailed(string paymentIntentId)
        {
            var spec = new OrderByPaymentIntentIdSpecification(paymentIntentId);
            var order = await _unitOfWork.Repository<Order>().GetEntityWithSpec(spec);
            if (order == null)
            {
                return null;
            }
            order.Status = OrderStatus.PaymentFailed;
            _unitOfWork.Repository<Order>().Update(order);
            await _unitOfWork.Complete();
            return order;
        }

        public async Task<Order> UpdatePaymentSucceeded(string paymentIntentId)
        {
            var spec = new OrderByPaymentIntentIdSpecification(paymentIntentId);
            var order = await _unitOfWork.Repository<Order>().GetEntityWithSpec(spec);
            if (order == null)
            {
                return null;
            }
            order.Status = OrderStatus.PaymentRecevied;
            _unitOfWork.Repository<Order>().Update(order);
            await _unitOfWork.Complete();
            return order;
        }
    }
}
