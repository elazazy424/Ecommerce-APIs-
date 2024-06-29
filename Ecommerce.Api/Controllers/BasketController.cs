using AutoMapper;
using Ecommerce.Api.Dtos;
using Ecommerce.BLL.Interfaces;
using Ecommerce.DAL.Entites;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IBasketRepository _basketRepository;
        public BasketController(IBasketRepository basketRepository,IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasketById(string id)
        {
            var basket = await _basketRepository.GetBasketAsync(id);
            return Ok(basket ?? new CustomerBasket(id));
        }

        [HttpPost]
        public async Task<ActionResult <CustomerBasket>> UpdateBasket(CustomerBasketDto customerBasketDto)
        {
            var basket = _mapper.Map<CustomerBasket>(customerBasketDto);
            var updatedBasket = await _basketRepository.UpdateBasketAsync(basket);
            return Ok(updatedBasket);
        }

        [HttpDelete]
        public async Task DeleteBasket(string id)
        {
            await _basketRepository.DeleteBasketAsync(id);
        }
    }
}
