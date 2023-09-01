using Microsoft.AspNetCore.Identity;

namespace respapi.eshop.Models.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public string KnownAs { get; set; }
        public ICollection<UserAdress> Adresses { get; set; }
    }
}
