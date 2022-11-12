using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tabkhity.Core.ResponsesTypes;
using Tabkhity.Services.Contracts;
using Tabkhity.Services.DTOs.Lunch;

namespace Tabkhity.Controllers
{
    [Authorize]
    public class LunchController : BaseApiController
    {
        private readonly ILunchService _lunchService;

        public LunchController(ILunchService lunchService)
        {
            _lunchService = lunchService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(OperationResult<LunchToReturnDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Post(LunchToSaveDto dto)
        {
            return ProcessResponse(await _lunchService.AddLunchAsync(dto));
        }

        [HttpGet]
        [ProducesResponseType(typeof(PagedResponse<List<LunchToReturnDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromQuery]GetAllLunchesRequestModel request)
        {
            return ProcessResponse(await _lunchService.GetAllLunchesAsync(request));
        }

        [HttpGet("for-user")]
        [ProducesResponseType(typeof(PagedResponse<List<LunchToReturnDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllLunchesForUser([FromQuery] GetAllLunchesForUserRequestModel request)
        {
            return ProcessResponse(await _lunchService.GetAllLunchesForUserAsync(request));
        }
        
        [HttpGet("for-current-user")]
        [ProducesResponseType(typeof(PagedResponse<List<LunchToReturnDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllLunchesForCurrentUser([FromQuery] GetAllLunchesRequestModel request)
        {
            return ProcessResponse(await _lunchService.GetAllLunchesForCurrentUserAsync(request));
        }
    }
}
