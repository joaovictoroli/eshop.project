namespace respapi.eshop.Models.DTOs
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<SubCategoryDto> SubCategories { get; set; }
    }
}
