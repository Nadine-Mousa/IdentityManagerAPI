using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using IdentityManagerAPI.Models.Authentication;
using Microsoft.EntityFrameworkCore;
using IdentityManagerAPI.Models;
using MimeKit;
using MailKit.Net.Smtp;
using User.Services.Management.EmailService.Models;
using MimeKit.Text;


namespace IdentityManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly EmailConfiguration _emailConfiguration;
        public AuthenticationController(
            UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, EmailConfiguration emailConfiguration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _emailConfiguration = emailConfiguration;

        }

        [HttpPost("/Register")]

        public async Task<IActionResult> Register(RegisterUserVM user, string role)
        {
            // Assign Username if not found
            if(user.Username == string.Empty || user.Username == null)
            {
                user.Username = new System.Net.Mail.MailAddress(user.Email).User;
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

                    //Add Token to Verify the email....
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(identity_user);
                    var confirmationLink = Url.Action(nameof(ConfirmEmail), "Authentication", new { token, email = user.Email }, Request.Scheme);
                    var message = new Message(new string[] { user.Email! }, "Confirmation email link", confirmationLink!);


                    // send email confirmation message
                    await SendConfirmationEmail(user.Email, message);



                    return StatusCode(StatusCodes.Status201Created, new Response("Success", $"User created & Email Sent to {user.Email} SuccessFully"));
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new Response("Error", "User failed to create"));
                }

            }
            else  return StatusCode(StatusCodes.Status500InternalServerError, new Response("Error", "Role name doesn't exist"));
            
        }

        [HttpGet("/SendConfirmationEmail")]
        public async Task<IActionResult> SendConfirmationEmail(string email, Message msg)
        {
            var message = new MimeMessage();
            message.Subject = "Email Configuration";
            message.From.Add(MailboxAddress.Parse(_emailConfiguration.From));
            message.To.Add(MailboxAddress.Parse(email));
            message.Body = new TextPart(TextFormat.Html) { Text = $"Please Confirm your account by clicking here {msg.Content}" };

            using var client = new SmtpClient();
            try
            {
                client.Connect(_emailConfiguration.SmtpServer, _emailConfiguration.Port, true);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate(_emailConfiguration.Username, _emailConfiguration.Password);

                client.Send(message);
            }
            catch
            {
                //log an error message or throw an exception or both.
                throw;
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }

            return StatusCode(StatusCodes.Status200OK, new Response("Success", "Verification email sent"));
        }

        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                    return StatusCode(StatusCodes.Status200OK,
                      new Response { Status = "Success", Message = "Email Verified Successfully" });
                }
            }
            return StatusCode(StatusCodes.Status500InternalServerError,
                       new Response { Status = "Error", Message = "This User Does not exist!" });
        }



    }
}
