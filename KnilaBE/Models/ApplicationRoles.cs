using Microsoft.AspNetCore.Identity;

namespace KnilaBE.Models
{
    public class ApplicationRoles:IdentityRole
    {
        public const string Admin = "Admin";
        public const string User = "User";
    }
}
