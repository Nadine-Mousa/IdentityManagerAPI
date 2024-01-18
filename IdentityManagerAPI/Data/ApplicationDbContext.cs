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
                new IdentityRole { Name = SD.Role_Admin, ConcurrencyStamp = "1", NormalizedName = "ADMIN" },
                new IdentityRole { Name = SD.Role_User, ConcurrencyStamp = "2", NormalizedName = "USER" },
                new IdentityRole { Name = SD.Role_HR, ConcurrencyStamp = "3", NormalizedName = "HR" });
                   
        }
    }
}
