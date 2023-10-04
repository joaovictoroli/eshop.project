using respapi.eshop.Models.Entities;

namespace respapi.eshop.Models.DTOs
{
    public class UserDetailsDto
    {
        public string? Username { get; set; }
        public string? KnownAs { get; set; }
        public ICollection<AddressDto>? Addresses { get; set; }
    }
}
