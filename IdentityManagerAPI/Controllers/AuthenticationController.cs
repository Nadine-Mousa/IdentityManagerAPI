using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using IdentityManagerAPI.Models.Authentication;
using System.Net.Mail;
using Microsoft.EntityFrameworkCore;
using IdentityManagerAPI.Models;

namespace IdentityManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AuthenticationController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpPost("/Register")]

        public async Task<IActionResult> Register(RegisterUserVM user, string role)
        {
            // Assign Username if not found
            if(user.Username == string.Empty || user.Username == null)
            {
                user.Username = new MailAddress(user.Email).User;
            }

            // Check if the user already exists
            bool user_exists = await _userManager.Users.FirstOrDefaultAsync(
                e => e.Email == user.Email || e.UserName == user.Username) != null;
            if(user_exists)
            {
                return StatusCode(StatusCodes.Status403Forbidden, new Response("Error", "User already exists"));
            }

            // Add role to user 
            var role_exists = await _roleManager.RoleExistsAsync(role);
            if (role_exists == true)
            {
                // Add user to db
                var identity_user = new IdentityUser
                {
                    Email = user.Email,
                    UserName = user.Username,
                    SecurityStamp = Guid.NewGuid().ToString(),
                };

                var result = await _userManager.CreateAsync(identity_user, user.Password);


                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(identity_user, role);
                    return StatusCode(StatusCodes.Status201Created, new Response("Success", "User Created Successfully."));
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new Response("Error", "User failed to create"));
                }

            }
            else  return StatusCode(StatusCodes.Status500InternalServerError, new Response("Error", "Role name doesn't exist"));
            
        }

        [HttpGet]
        public void singin()
        {
            // sing in by email or password
        }

    }
}
