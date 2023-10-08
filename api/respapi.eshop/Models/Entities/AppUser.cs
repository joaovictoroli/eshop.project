using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace respapi.eshop.Models.Entities;
public class AppUser : IdentityUser<int>
{    
    [MinLength(4, ErrorMessage = "The length of {0} must be at least {1} characters.")]
    [MaxLength(15, ErrorMessage = "The length of {0} cannot be more than {1} characters.")]
    public string? KnownAs { get; set; }
    public ICollection<UserAddress>? Addresses { get; set; }
    public ICollection<AppUserRole>? UserRoles { get; set; }
    public ICollection<Order>? Orders { get; set; }
}