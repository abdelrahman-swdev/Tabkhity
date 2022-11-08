using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tabkhity.Errors;
using Tabkhity.Services.Contracts;
using Tabkhity.Services.DTOs.Lunch;

namespace Tabkhity.Controllers
{
    public class LunchController : BaseApiController
    {
        private readonly ILunchService _lunchService;

        public LunchController(ILunchService lunchService)
        {
            _lunchService = lunchService;
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(LunchToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiValidationErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post(LunchToSaveDto dto)
        {
            var result = await _lunchService.AddLunchAsync(dto);
            return result is null ? BadRequest(new ApiResponse(400)) : Ok(result);
        }
    }
}
