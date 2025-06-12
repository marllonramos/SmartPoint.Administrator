using Microsoft.AspNetCore.Identity;

namespace SmartPoint.Administrator.Infra.Identity.Entity
{
    public class ApplicationUser : IdentityUser
    {
        public bool Active { get; set; } = true;
        //public string? FullName { get; set; }
    }
}
