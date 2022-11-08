using Tabkhity.Core.Interfaces;
using Tabkhity.Infrastructure.Data.Repositories;
using Tabkhity.Services.Contracts;
using Tabkhity.Services.Implementation;

namespace Tabkhity.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ILunchService, LunchService>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddHttpContextAccessor();

            return services;
        }
    }
}
