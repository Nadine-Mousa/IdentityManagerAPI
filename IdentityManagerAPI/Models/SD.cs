namespace IdentityManagerAPI.Models
{
    public class SD
    {
        public const string Role_Admin = "Admin";
        public const string Role_User = "User";
        public const string Role_HR = "HR";
        public static List<string> roles = new List<string> { Role_Admin, Role_User, Role_HR};

    }
}
