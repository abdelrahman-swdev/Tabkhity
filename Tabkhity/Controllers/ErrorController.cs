using Microsoft.AspNetCore.Mvc;
using Tabkhity.Errors;

namespace Tabkhity.Controllers
{
    [Route("errors/{code}")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : BaseApiController
    {
        public IActionResult Error(int code) =>
             new ObjectResult(new ApiResponse(code));
    }
}
