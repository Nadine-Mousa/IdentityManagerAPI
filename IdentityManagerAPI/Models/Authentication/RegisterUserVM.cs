using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IdentityManagerAPI.Models.Authentication
{
    public class RegisterUserVM
    {

        public string? Username { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }

}
