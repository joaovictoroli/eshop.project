using System.ComponentModel.DataAnnotations;

namespace respapi.eshop.Models.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string? Username { get; set; }
        [Required]
        public string? KnownAs { get; set; }
        [Required]
        [StringLength(8, MinimumLength = 4)]
        public string? Password { get; set; }
    }
}
