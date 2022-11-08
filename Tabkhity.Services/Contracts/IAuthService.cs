using Tabkhity.Core.Identity;
using Tabkhity.Services.DTOs;

namespace Tabkhity.Services.Contracts
{
    public interface IAuthService
    {
        Task<UserToReturnDto> LoginAsync(LoginDto loginDto);
        Task<UserToReturnDto> RegisterAsync(RegisterDto registerDto);
        Task<bool> CheckEmailExistsAsync(string email);
        Task<ApplicationUser> GetCurrentUser();
    }
}
