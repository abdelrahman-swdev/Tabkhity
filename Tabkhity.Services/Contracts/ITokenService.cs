using Tabkhity.Core.Identity;

namespace Tabkhity.Services.Contracts
{
    public interface ITokenService
    {
        string CreateToken(ApplicationUser user);
    }
}
