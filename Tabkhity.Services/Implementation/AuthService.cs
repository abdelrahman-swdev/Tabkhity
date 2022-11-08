using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Tabkhity.Core.Identity;
using Tabkhity.Services.Contracts;
using Tabkhity.Services.DTOs;

namespace Tabkhity.Services.Implementation
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITokenService _tokenService;
        private ApplicationUser _currentUser;

        public AuthService(UserManager<ApplicationUser> userManager,
         SignInManager<ApplicationUser> signInManager,
         IHttpContextAccessor httpContextAccessor,
         ITokenService tokenService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _tokenService = tokenService;
        }

        public async Task<ApplicationUser> GetCurrentUser()
        {
            if (_currentUser != null)
                return _currentUser;

            if(_httpContextAccessor.HttpContext is not null)
            {
                if(_httpContextAccessor.HttpContext.Items["LoggedInUser"] is not null)
                    return _httpContextAccessor.HttpContext.Items["LoggedInUser"] as ApplicationUser;

                var userId = _httpContextAccessor.HttpContext != null 
                    ? _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier) 
                    : string.Empty;

                var user = await _userManager.FindByIdAsync(userId);
                if (user is not null)
                {
                    _currentUser = user;
                    _httpContextAccessor.HttpContext.Items["LoggedInUser"] = user;
                }
            }
               
            if (_currentUser is null) 
                throw new Exception("The user was not exist!");

            return _currentUser;
        }

        public async Task<bool> CheckEmailExistsAsync(string email) =>
             await _userManager.FindByEmailAsync(email) != null;

        public async Task<UserToReturnDto> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user is null) return null;

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded) return null;

            return new UserToReturnDto
            {
                Email = user.Email,
                Name = user.Name,
                Token = _tokenService.CreateToken(user)
            };
        }

        public async Task<UserToReturnDto> RegisterAsync(RegisterDto registerDto)
        {
            var user = new ApplicationUser
            {
                Name = registerDto.Name,
                Email = registerDto.Email,
                UserName = registerDto.Email
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded) return null;

            return new UserToReturnDto
            {
                Email = user.Email,
                Name = user.Name,
                Token = _tokenService.CreateToken(user)
            };
        }
    }
}
