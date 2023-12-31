﻿using System.ComponentModel.DataAnnotations;

namespace respapi.eshop.Models.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }       
        [Required]
        public float Price { get; set; }
        [Required]
        public string TechnicalInfo { get; set; }
        public string? ImageUrl { get; set; }
        public int? SubCategoryId { get; set; }        
    }
}
