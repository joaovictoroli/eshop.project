using Microsoft.AspNetCore.Identity;

namespace respapi.eshop.Models.Entities
{
    public class AppRole : IdentityRole<int>
    {
        public ICollection<AppUserRole>? UserRoles { get; set; }
    }
}
