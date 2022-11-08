using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Tabkhity.Core.Entities;
using Tabkhity.Core.Identity;

namespace Tabkhity.Infrastructure.Data
{
    public class TabhkityDbContext : IdentityDbContext<ApplicationUser>
    {
        public TabhkityDbContext(DbContextOptions<TabhkityDbContext> options) : base(options)
        {
        }

        public DbSet<Lunch> Lunches { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
