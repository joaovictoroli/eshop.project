using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace respapi.eshop.Models.Entities
{
    public class Product
    {        
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public float Price { get; set; }
        public string TechnicalInfo { get; set; }
        public SubCategory? SubCategory { get; set; }
        public int SubCategoryId { get; set; }
    }
}
