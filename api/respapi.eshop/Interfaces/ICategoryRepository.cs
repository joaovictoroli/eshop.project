using respapi.eshop.Models.DTOs;
using respapi.eshop.Models.Entities;

namespace respapi.eshop.Interfaces
{
    public interface ICategoryRepository
    {
        Task<int> AddCategory(Category category);
        Task<List<CategoryDto>> GetAllCategories();
        Task<Category> UpdateCategory(Category category);
        Task<Category> DeleteCategory(int id);
        
        // sub categories
        Task<List<SubCategory>> GetAllSubCategories();
        Task<int> AddSubCategory(SubCategory subCategoryn, int categoryId);

        Task<SubCategory> GetSubCategoryByName(string subCategoryName);
        Task<SubCategory> GetSubCategoryById(int categoryId);

    }
}
