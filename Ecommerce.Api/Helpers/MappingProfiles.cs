using AutoMapper;
using Ecommerce.Api.Dtos;
using Ecommerce.DAL.Entites;
using Ecommerce.DAL.Entites.OrderAggregate;
namespace Ecommerce.Api.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
                .ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name))
                .ForMember(d => d.ImageUrl, o => o.MapFrom<ProductUrlResolver>());
            CreateMap<DAL.Identity.Address, AddressDto>().ReverseMap();
            CreateMap<CustomerBasketDto, CustomerBasket>().ReverseMap();
            CreateMap<BasketItemDto, BasketItem>().ReverseMap();
            CreateMap<AddressDto, DAL.Entites.OrderAggregate.Address>().ReverseMap();

            CreateMap<Order, OrderToReturnDto>()
                .ForMember(d => d.DeliveryMethod, o => o.MapFrom(s => s.DeliveryMethod.ShortName))
                .ForMember(d => d.ShippingPrice, o => o.MapFrom(s => s.DeliveryMethod.Price));

            CreateMap<OrderItem, OrderItemDto>()
               .ForMember(d => d.ProductId, o => o.MapFrom(s => s.ProductItemOrder.ProductId))
               .ForMember(d => d.ProductName, o => o.MapFrom(s => s.ProductItemOrder.ProductName))
               .ForMember(d => d.PictureUrl, o => o.MapFrom(s => s.ProductItemOrder.PictureUrl))
               .ForMember(d => d.PictureUrl, o => o.MapFrom<OrderItemUrlResolver>());
        }
    }
}
