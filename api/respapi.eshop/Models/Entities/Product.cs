using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace respapi.eshop.Models.Entities
{
    public class Product
    {        
        [Key]
        public int Id { get; set; }
        [Required]
        [MinLength(4), MaxLength(250)]
        public string Name { get; set; }
        [Required]
        [MinLength(4)]
        public string Description { get; set; }        
        public string ImageUrl { get; set; }
        
        [Required]      
        public float Price { get; set; }
        [Required]
        [MinLength(4)]
        public string TechnicalInfo { get; set; }
        public SubCategory? SubCategory { get; set; }
        public int SubCategoryId { get; set; }
    }
}
