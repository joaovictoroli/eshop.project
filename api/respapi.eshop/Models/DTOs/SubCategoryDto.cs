﻿using System.ComponentModel.DataAnnotations;

namespace respapi.eshop.Models.DTOs
{
    public class SubCategoryDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }
}
