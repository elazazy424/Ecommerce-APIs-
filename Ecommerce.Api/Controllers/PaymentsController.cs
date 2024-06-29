using Ecommerce.Api.Errors;
using Ecommerce.BLL.Interfaces;
using Ecommerce.BLL.Services;
using Ecommerce.BLL.Specifications;
using Ecommerce.DAL.Entites;
using Ecommerce.DAL.Entites.OrderAggregate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Stripe;

namespace Ecommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : BaseApiController
    {
        private readonly IPaymentService _paymentService;
        private const string WhSecret = "whsec_b4f4cdc1ebd0bd9848b20334a72a778e8bfc0e0eb26e28890c994be74468b5b4";
        private readonly ILogger<PaymentsController> _logger;

        public PaymentsController(IPaymentService paymentService, ILogger<PaymentsController> logger)
        {
            _paymentService = paymentService;
            _logger = logger;
        }
        #region CreateOrUpdatePaymentIntent
        [Authorize]
        [HttpPost("{basketId}")]
        public async Task<ActionResult<CustomerBasket>> CreateOrUpdatePaymentIntent(string basketId)
        {
            var basket = await _paymentService.createOrUpdatePaymentIntent(basketId);
            if (basket == null)
            {
                return BadRequest(new ApiResponse(400, "Problem With Your Basket"));
            }
            return basket;
        }
        #endregion
        #region webhook
        [HttpPost("webhook")]
        public async Task<ActionResult> StripeWebHook()
        {
            var json = await new StreamReader(Request.Body).ReadToEndAsync();

            var stripeEvent = EventUtility.ConstructEvent(json, Request.Headers["Stripe-Signature"], WhSecret);

            PaymentIntent intent;

            Order order;

            switch (stripeEvent.Type)
            {
                case "payment_intent.succeeded":
                   intent = (PaymentIntent)stripeEvent.Data.Object;
                    _logger.LogInformation("Payment Succeeded: ", intent.Id);
                    order = await _paymentService.UpdatePaymentSucceeded(intent.Id);
                    _logger.LogInformation("Order updated to payment received: ", order.Id);
                    break;

                case "payment_intent.payment_failed":
                    intent = (PaymentIntent)stripeEvent.Data.Object;
                    _logger.LogInformation("Payment Failed: ", intent.Id);
                    order = await _paymentService.UpdatePaymentFailed(intent.Id);
                    _logger.LogInformation("Order updated to Payment Failed: ", order.Id);
                    break;
            }
            return new EmptyResult();
        }
        #endregion
    }
}
