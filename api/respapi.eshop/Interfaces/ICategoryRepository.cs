using respapi.eshop.Models.DTOs;
using respapi.eshop.Models.Entities;

namespace respapi.eshop.Interfaces
{
    public interface ICategoryRepository
    {
        Task<string> AddCategory(Category category);
        Task<string> AddSubCategory(SubCategory subCategoryn, int categoryId);
        Task<List<CategoryDto>> GetAllCategories();
        Task<string> DeleteCategory(int id);

        Task<string> DeleteSubCategory(int id);
        
        // sub categories
        Task<List<SubCategory>> GetAllSubCategories();


        Task<SubCategory> GetSubCategoryByName(string subCategoryName);
    }
}
