using AutoMapper;
using Ecommerce.Api.Dtos;
using Ecommerce.Api.Errors;
using Ecommerce.Api.Extensions;
using Ecommerce.BLL.Interfaces;
using Ecommerce.DAL.Entites.OrderAggregate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : BaseApiController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        public OrdersController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }
        #region create order
        [HttpPost("CreateOrder")]
        public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto)
        {
            var email = HttpContext.User?.RetrieveEmailFromPrincipal();

            var address = _mapper.Map<AddressDto, Address>(orderDto.ShippingAddress);

            var order = await _orderService.CreateOrderAsync(email, orderDto.DeliveryMethodId, orderDto.BasketId, address);

            if (order == null) return BadRequest(new ApiResponse(400, "Problem creating order"));

            return Ok(order);
        }
        #endregion
        #region get orders for user
        [HttpGet("GetOrdersForUser")]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetOrdersForUser()
        {
            var email = HttpContext.User?.RetrieveEmailFromPrincipal();

            var orders = await _orderService.GetOrdersForUserAsync(email);
            return Ok(_mapper.Map<IReadOnlyList<OrderToReturnDto>>(orders));

        }
        #endregion
        #region get order by id
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderToReturnDto>> GetOrderById(int id)
        {
            var email = HttpContext.User?.RetrieveEmailFromPrincipal();
            var order = await _orderService.GetOrderByIdAsync(id, email);
            if (order == null) return NotFound(new ApiResponse(404));
            return _mapper.Map<OrderToReturnDto>(order);
        }
        #endregion
        #region get delivery methods
        [HttpGet("GetDeliveryMethods")]
        public async Task<ActionResult<IReadOnlyList<DeleiveryMethod>>> GetDeliveryMethods()
        {
            return Ok(await _orderService.GetDeliveryMethodsAsync());
        }
        #endregion
    }
}
