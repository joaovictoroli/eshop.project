using System.ComponentModel.DataAnnotations;

namespace respapi.eshop.Models.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string? Username { get; set; }
        [Required]
        [MinLength(4, ErrorMessage = "The length of {0} must be at least {1} characters.")]
        [MaxLength(15, ErrorMessage = "The length of {0} cannot be more than {1} characters.")]
        public string? KnownAs { get; set; }
        [Required]
        [StringLength(8, MinimumLength = 4)]
        public string? Password { get; set; }
    }
}
