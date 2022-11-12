using Tabkhity.Core.ResponsesTypes;
using Tabkhity.Services.DTOs.Lunch;

namespace Tabkhity.Services.Contracts
{
    public interface ILunchService
    {
        Task<OperationResult<LunchToReturnDto>> AddLunchAsync(LunchToSaveDto dto);
        Task<PagedResponse<List<LunchToReturnDto>>> GetAllLunchesAsync(GetAllLunchesRequestModel request);
        Task<PagedResponse<List<LunchToReturnDto>>> GetAllLunchesForUserAsync(GetAllLunchesForUserRequestModel request);
        Task<PagedResponse<List<LunchToReturnDto>>> GetAllLunchesForCurrentUserAsync(GetAllLunchesRequestModel request);
    }
}
