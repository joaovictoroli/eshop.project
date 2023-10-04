using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace respapi.eshop.Models.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public string? KnownAs { get; set; }
        public ICollection<UserAddress>? Addresses { get; set; }
        public ICollection<AppUserRole>? UserRoles { get; set; }
    }
}
