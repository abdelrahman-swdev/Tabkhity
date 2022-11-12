using Microsoft.AspNetCore.Mvc;
using Tabkhity.Core.Errors;
using Tabkhity.Core.ResponsesTypes;

namespace Tabkhity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        protected ActionResult ProcessResponse(HttpErrorCodes errorCode, string erroMessage = "")
        {
            return StatusCode((int)errorCode, new { 
                code = CommonErrorCodes.NULL.Value, 
                erroMessage 
            });
        }

        protected ActionResult ProcessResponse<T>(OperationResult<T> response)
        {
            return response.IsSucceeded 
                ? Ok(response.Data) 
                : StatusCode((int)response.HttpErrorCode, new { 
                    code = response.Code.Code, 
                    value = response.Code.Value, 
                    response.ErrorMessage 
                });
        }

        protected ActionResult ProcessResponse<T>(PagedResponse<T> response)
        {
            return response.IsSucceeded 
                ? Ok(response) 
                : StatusCode((int)response.HttpErrorCode, new { 
                    code = response.Code.Code, 
                    value = response.Code.Value, 
                    response.ErrorMessage 
                });
        }
    }
}
