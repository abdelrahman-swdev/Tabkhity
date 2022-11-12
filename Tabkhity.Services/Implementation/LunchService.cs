using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tabkhity.Core.Entities;
using Tabkhity.Core.Errors;
using Tabkhity.Core.Identity;
using Tabkhity.Core.Interfaces;
using Tabkhity.Core.ResponsesTypes;
using Tabkhity.Services.Contracts;
using Tabkhity.Services.DTOs.Lunch;

namespace Tabkhity.Services.Implementation
{
    public class LunchService : ILunchService
    {
        private readonly IGenericRepository<Lunch> _lunchRepository;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;
        public LunchService(IGenericRepository<Lunch> lunchRepository, IAuthService authService, IMapper mapper)
        {
            _lunchRepository = lunchRepository;
            _authService = authService;
            _mapper = mapper;
        }
        public async Task<OperationResult<LunchToReturnDto>> AddLunchAsync(LunchToSaveDto dto)
        {
            try
            {

                var user = await _authService.GetCurrentUser();
                if (user is null)
                    return OperationResult<LunchToReturnDto>.Fail(HttpErrorCodes.NotAuthorized);

                Lunch addedLunch = await AddLunchToDB(dto, user);

                if (addedLunch is null)
                    return OperationResult<LunchToReturnDto>.Fail(HttpErrorCodes.ServerError);


                return OperationResult<LunchToReturnDto>.Success(_mapper.Map<LunchToReturnDto>(addedLunch));
            }
            catch (Exception ex)
            {
                return GetOperationResultServerError(ex);
            }
        }

        private static OperationResult<LunchToReturnDto> GetOperationResultServerError(Exception ex)
        {
            return OperationResult<LunchToReturnDto>.ServerError(ex);
        }

        private async Task<Lunch> AddLunchToDB(LunchToSaveDto dto, ApplicationUser user)
        {
            return await _lunchRepository.AddAsync(new Lunch
            {
                UserId = user.Id,
                CreationDate = DateTime.Now,
                Name = dto.Name,
            });
        }

        public async Task<PagedResponse<List<LunchToReturnDto>>> GetAllLunchesAsync(GetAllLunchesRequestModel request)
        {
            try
            {
                List<Lunch> lunches = await GetPagedLunches(request);

                if (lunches.Count == 0)
                    return new PagedResponse<List<LunchToReturnDto>>(null, request.PageNumber, request.PageSize);

                var totalCount = await _lunchRepository.CountAsync(x => true);

                var mappedData = _mapper.Map<List<LunchToReturnDto>>(lunches);
                return new PagedResponse<List<LunchToReturnDto>>(mappedData, request.PageNumber, request.PageSize, totalCount);

            }
            catch (Exception)
            {
                return GetPagedResponseServerError();
            }
        }

        private static PagedResponse<List<LunchToReturnDto>> GetPagedResponseServerError()
        {
            return new PagedResponse<List<LunchToReturnDto>>(HttpErrorCodes.ServerError);
        }

        private async Task<List<Lunch>> GetPagedLunches(GetAllLunchesRequestModel request)
        {
            return await _lunchRepository
                                .FindBy(l => true)
                                .OrderByDescending(l => l.CreationDate)
                                .Skip((request.PageNumber - 1) * request.PageSize)
                                .Take(request.PageSize)
                                .ToListAsync();
        }
        
        private async Task<List<Lunch>> GetPagedLunchesForUser(GetAllLunchesForUserRequestModel request)
        {
            return await _lunchRepository
                                .FindBy(l => l.UserId == request.UserId)
                                .OrderByDescending(l => l.CreationDate)
                                .Skip((request.PageNumber - 1) * request.PageSize)
                                .Take(request.PageSize)
                                .ToListAsync();
        }

        public async Task<PagedResponse<List<LunchToReturnDto>>> GetAllLunchesForUserAsync(GetAllLunchesForUserRequestModel request)
        {
            try
            {
                var user = await _authService.GetCurrentUser();
                if (user is null)
                    return new PagedResponse<List<LunchToReturnDto>>(HttpErrorCodes.NotAuthorized);

                List<Lunch> lunchesForUser = await GetPagedLunchesForUser(request);

                if (lunchesForUser.Count == 0)
                    return new PagedResponse<List<LunchToReturnDto>>(null, request.PageNumber, request.PageSize);

                var totalCount = await _lunchRepository.CountAsync(x => true);

                var mappedData = _mapper.Map<List<LunchToReturnDto>>(lunchesForUser);
                return new PagedResponse<List<LunchToReturnDto>>(mappedData, request.PageNumber, request.PageSize, totalCount);

            }
            catch (Exception)
            {
                return GetPagedResponseServerError();
            }
        }
        
        public async Task<PagedResponse<List<LunchToReturnDto>>> GetAllLunchesForCurrentUserAsync(GetAllLunchesRequestModel request)
        {
            try
            {
                var user = await _authService.GetCurrentUser();
                if (user is null)
                    return new PagedResponse<List<LunchToReturnDto>>(HttpErrorCodes.NotAuthorized);

                List<Lunch> lunchesForUser = await GetPagedLunchesForUser(new GetAllLunchesForUserRequestModel
                {
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize,
                    UserId = user.Id
                });

                if (lunchesForUser.Count == 0)
                    return new PagedResponse<List<LunchToReturnDto>>(null, request.PageNumber, request.PageSize);

                var totalCount = await _lunchRepository.CountAsync(x => true);

                var mappedData = _mapper.Map<List<LunchToReturnDto>>(lunchesForUser);
                return new PagedResponse<List<LunchToReturnDto>>(mappedData, request.PageNumber, request.PageSize, totalCount);

            }
            catch (Exception)
            {
                return GetPagedResponseServerError();
            }
        }
    }
}
