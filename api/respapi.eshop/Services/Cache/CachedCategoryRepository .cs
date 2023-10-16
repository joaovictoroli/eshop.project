using System.Text.Json;
using respapi.eshop.Interfaces;
using respapi.eshop.Models.DTOs;
using respapi.eshop.Models.Entities;
using StackExchange.Redis;

namespace respapi.eshop.Services.Cache;

public class CachedCategoryRepository : ICategoryRepository
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IDatabase _cache;

    public CachedCategoryRepository(ICategoryRepository innerRepository, IDatabase cache)
    {
        _categoryRepository = innerRepository;
        _cache = cache;
    }

    public async Task<List<CategoryDto>> GetAllCategories()
    {
        var cacheKey = "all-categories";

        var cachedCategories = _cache.StringGet(cacheKey);
        if (!cachedCategories.IsNullOrEmpty)
        {
            return JsonSerializer.Deserialize<List<CategoryDto>>(cachedCategories);
        }

        var categories = await _categoryRepository.GetAllCategories();
        _cache.StringSet(cacheKey, JsonSerializer.Serialize(categories), TimeSpan.FromHours(1));

        return categories;
    }

 

    public async Task<string> AddCategory(Category category)
    {
        var result = await _categoryRepository.AddCategory(category);
       
         _cache.KeyDelete("all-categories");
        
        return result;
    }

    public async Task<string> AddSubCategory(SubCategory subCategory, int categoryId)
    {
        var result = await _categoryRepository.AddSubCategory(subCategory, categoryId);
        _cache.KeyDelete("all-categories"); 
        _cache.KeyDelete("all-subcategories");

        return result;
    }
    public async Task<string> DeleteCategory(int id)
    {
        var result = await _categoryRepository.DeleteCategory(id);
        _cache.KeyDelete("all-categories"); 
        return result;
    }

    public async Task<SubCategory> GetSubCategoryByName(string subCategoryName)
    {
        var cacheKey = "all-subcategories";

        List<SubCategory> allSubCategories;

    
        var cachedSubCategories = _cache.StringGet(cacheKey);
        if (!cachedSubCategories.IsNullOrEmpty)
        {
            allSubCategories = JsonSerializer.Deserialize<List<SubCategory>>(cachedSubCategories);
        }
        else
        {

            allSubCategories = await _categoryRepository.GetAllSubCategories();
            _cache.StringSet(cacheKey, JsonSerializer.Serialize(allSubCategories), TimeSpan.FromHours(1));
        }

       return allSubCategories.FirstOrDefault(subCat => subCat.Name.Equals(subCategoryName, StringComparison.OrdinalIgnoreCase));

    }

    public async Task<string> DeleteSubCategory(int id)
    {
        var result = await _categoryRepository.DeleteSubCategory(id);
        _cache.KeyDelete("all-categories"); 
        _cache.KeyDelete("all-subcategories");
        return result;
    }

    public Task<List<SubCategory>> GetAllSubCategories()
    {
        throw new NotImplementedException();
    }
} 