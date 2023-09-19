﻿namespace respapi.eshop.Models.Entities
{
    public class SubCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}