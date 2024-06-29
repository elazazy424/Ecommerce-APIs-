using AutoMapper;
using Ecommerce.Api.Dtos;
using Ecommerce.DAL.Entites;

namespace Ecommerce.Api.Helpers
{
    public class ProductUrlResolver : IValueResolver<Product, ProductDto, string>
    {
        private readonly IConfiguration _config;
        public ProductUrlResolver(IConfiguration configuration)
        {
            _config = configuration;
        }
        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.ImageUrl))
            {
                return _config["ApiUrl"] + source.ImageUrl;
            }
            return null;
        }
    }
}
