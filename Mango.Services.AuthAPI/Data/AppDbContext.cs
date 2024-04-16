
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.AuthAPI.Data
{
    // Represents the application's database context
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        // Initializes a new instance of the AppDbContext class
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Configures the model for the database
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
        }
    }
}
