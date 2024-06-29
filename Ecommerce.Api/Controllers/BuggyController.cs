using Ecommerce.Api.Errors;
using Ecommerce.DAL.Data;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuggyController : BaseApiController
    {
        private readonly ApplicationDbContext _context;
        public BuggyController(ApplicationDbContext context)
        {
            _context = context;
        }
        #region get NotFound
        [HttpGet("notFound")]
        public IActionResult GetNotFoundRequest()
        {
            var thing = _context.Products.Find(50);
            if (thing == null)
            {
                return NotFound(new ApiResponse(404));
            }
            return Ok(thing);
        }
        #endregion
        #region get Server Error
        [HttpGet("serverError")]
        public IActionResult GetServerError()
        {
            var thing = _context.Products.Find(50);
            var thingtoReturn = thing.ToString();
            return Ok(thingtoReturn);
        }
        #endregion
        #region get BadRequest
        [HttpGet("badRequest")]
        public IActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(400));
        }
        #endregion
        #region get BadRequest with Id
        [HttpGet("badrequest/{id}")]
        public IActionResult GetNotFoundRequest(int id)
        {
            return Ok();
        }
        #endregion
    }
}
