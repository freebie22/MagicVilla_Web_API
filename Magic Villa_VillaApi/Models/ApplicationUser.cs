using Microsoft.AspNetCore.Identity;

namespace Magic_Villa_VillaApi.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
