﻿using System.ComponentModel.DataAnnotations;

namespace respapi.eshop.Models.Entities
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public ICollection<SubCategory>? SubCategories { get; set; }
    }
}
