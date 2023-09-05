using System.ComponentModel.DataAnnotations;

namespace respapi.eshop.Models.DTOs
{
    public class ImageUploadDto
    {
        [Required]
        public IFormFile File { get; set; }
        public string? FileDescription { get; set; }
    }
}
