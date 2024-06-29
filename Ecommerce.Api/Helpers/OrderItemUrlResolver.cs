using AutoMapper;
using Ecommerce.Api.Dtos;
using Ecommerce.DAL.Entites;
using Ecommerce.DAL.Entites.OrderAggregate;

namespace Ecommerce.Api.Helpers
{
    public class OrderItemUrlResolver : IValueResolver<OrderItem, OrderItemDto, string>
    {
        private readonly IConfiguration _config;
        public OrderItemUrlResolver(IConfiguration configuration)
        {
            _config = configuration;
        }
        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.ProductItemOrder.PictureUrl))
            {
                return _config["ApiUrl"] + source.ProductItemOrder.PictureUrl;
            }
            return null;
        }
    }
}

