using IdentityManagerAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityManagerAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Seed User Roles
            SeedRoles(builder);
        }
        private void SeedRoles(ModelBuilder builder)
        {
               builder.Entity<IdentityRole>().HasData(
                   new IdentityRole(SD.Role_Admin),
                   new IdentityRole(SD.Role_HR),
                   new IdentityRole(SD.Role_User));
        }
    }
}
