using Microsoft.AspNetCore.Identity;

namespace Host.Data
{
    public class ApplicationUser : IdentityUser
    {
        public bool Enabled { get; set; } = true;
    }
}