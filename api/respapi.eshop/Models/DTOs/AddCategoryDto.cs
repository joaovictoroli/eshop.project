using System.ComponentModel.DataAnnotations;

namespace respapi.eshop.Models.DTOs
{
    public class AddCategoryDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
