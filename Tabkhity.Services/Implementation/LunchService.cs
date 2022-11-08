using Tabkhity.Core.Entities;
using Tabkhity.Core.Interfaces;
using Tabkhity.Services.Contracts;
using Tabkhity.Services.DTOs.Lunch;

namespace Tabkhity.Services.Implementation
{
    public class LunchService : ILunchService
    {
        private readonly IGenericRepository<Lunch> _lunchRepository;
        private readonly IAuthService _authService;
        public LunchService(IGenericRepository<Lunch> lunchRepository, IAuthService authService)
        {
            _lunchRepository = lunchRepository;
            _authService = authService;
        }
        public async Task<LunchToReturnDto> AddLunchAsync(LunchToSaveDto dto)
        {
            var user = await _authService.GetCurrentUser();
            if(user is null) return null;

            var addedLunch = await _lunchRepository.AddAsync(new Lunch
            {
                UserId = user.Id,
                CreationDate = DateTime.Now,
                Name = dto.Name,
            });

            return addedLunch is not null
                ? new LunchToReturnDto
                {
                    Id = addedLunch.Id,
                    UserId = addedLunch.UserId,
                    Name = addedLunch.Name,
                    CreationDate = addedLunch.CreationDate
                }
                : null;
        }
    }
}
