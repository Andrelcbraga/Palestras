using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Palestras.Models
{
    public class AppDbContext : IdentityDbContext

    {
        public AppDbContext(DbContextOptions<AppDbContext> options) :
            base(options)
        {

        }
       public DbSet<Palestrante> Palestrantes { get; set; }
    }
}
