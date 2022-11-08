using Tabkhity.Services.DTOs.Lunch;

namespace Tabkhity.Services.Contracts
{
    public interface ILunchService
    {
        Task<LunchToReturnDto> AddLunchAsync(LunchToSaveDto dto);
    }
}
