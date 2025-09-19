using Microsoft.AspNetCore.Identity;

namespace PhoenicianCapital.Entities
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }

        // Navigation property
 
    }
}
