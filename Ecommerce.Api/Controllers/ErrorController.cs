using Ecommerce.Api.Errors;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [Route("errors/{code}")]
    //lazem n3ml ignore ll api 3shan myb2ash y3ml generate ll error controller
    //tb lw m3mlnash kda el swagger h3ml generate ll error controller w m4 hy4t8l
    //leh m4 hy4t8l? 3shan el swagger by4t8l 3la el api controller w y3ml generate ll error controller
    //tb w el 7l da m4 7l 7lw 3shan el swagger by4t8l 3la el api controller w y3ml generate ll error controller
    //2omal eh el 7al lw m4 3awz 23mel ignore ll api controller? 
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : BaseApiController
    {
        public IActionResult Error(int code)
        {
            return new ObjectResult(new ApiResponse(code));
        }
    }
}
