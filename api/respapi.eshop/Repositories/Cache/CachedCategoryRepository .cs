using System;
using System.Text.Json;
using respapi.eshop.Interfaces;
using respapi.eshop.Models.DTOs;
using respapi.eshop.Models.Entities;
using StackExchange.Redis;

namespace respapi.eshop.Repositories.Cache;

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

    public async Task<List<SubCategory>> GetAllSubCategories()
    {
        var cacheKey = "all-subcategories";

        var cachedSubCategories = _cache.StringGet(cacheKey);
        if (!cachedSubCategories.IsNullOrEmpty)
        {
            return JsonSerializer.Deserialize<List<SubCategory>>(cachedSubCategories);
        }

        var subCategories = await _categoryRepository.GetAllSubCategories();
        _cache.StringSet(cacheKey, JsonSerializer.Serialize(subCategories), TimeSpan.FromHours(1));

        return subCategories;
    }

    public async Task<int> AddCategory(Category category)
    {
        var result = await _categoryRepository.AddCategory(category);
        
        // Após adicionar uma categoria, podemos querer invalidar o cache relacionado a categorias.
        _cache.KeyDelete("all-subcategories"); // Considerando que uma nova categoria pode influenciar na lista de subcategorias.
        
        return result;
    }

    public async Task<int> AddSubCategory(SubCategory subCategory, int categoryId)
    {
        var result = await _categoryRepository.AddSubCategory(subCategory, categoryId);

        // Após adicionar uma subcategoria, invalidamos o cache das subcategorias
        _cache.KeyDelete("all-subcategories");

        return result;
    }

    public Task<Category> UpdateCategory(Category category)
    {
        throw new NotImplementedException();
    }

    public Task<Category> DeleteCategory(int id)
    {
        throw new NotImplementedException();
    }

    public Task<SubCategory> GetSubCategoryByName(string subCategoryName)
    {
        throw new NotImplementedException();
    }

    public Task<SubCategory> GetSubCategoryById(int categoryId)
    {
        throw new NotImplementedException();
    }

    // Você faria algo semelhante para outros métodos, como GetAllSubCategories, GetSubCategoryByName, etc.

    // Para métodos como AddCategory, AddSubCategory, DeleteCategory e UpdateCategory, 
    // você apenas chamaria o método correspondente em _innerRepository 
    // e, em seguida, invalidaria o cache apropriado.

    //     public async Task<int> AddCategory(Category category)
    // {
    //     var result = await _innerRepository.AddCategory(category);

    //     // Invalidar o cache após adicionar a categoria
    //     _cache.KeyDelete("all-categories");

    //     return result;
    // }



}

