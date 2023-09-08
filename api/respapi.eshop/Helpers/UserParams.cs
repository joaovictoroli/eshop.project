namespace respapi.eshop.Helpers
{
    public class UserParams : PaginationParams
    {
        public string Name { get; set; } = string.Empty;
        public int MinPrice { get; set; } = 0;
        public int MaxPrice { get; set; } = 9999;
        public string SubCategoryName { get; set; } = string.Empty;
    }
}
