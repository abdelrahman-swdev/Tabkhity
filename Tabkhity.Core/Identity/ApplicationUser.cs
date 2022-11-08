using Microsoft.AspNetCore.Identity;

namespace Tabkhity.Core.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
