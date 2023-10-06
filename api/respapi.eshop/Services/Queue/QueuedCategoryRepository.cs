using System;
using System.Text.Json;
using respapi.eshop.Interfaces;
using respapi.eshop.Models.DTOs;
using respapi.eshop.Models.Entities;

namespace respapi.eshop.Services.Queue;

public class QueuedCategoryRepository : ICategoryRepository
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMessageQueueService _messageQueueService;

    public QueuedCategoryRepository(ICategoryRepository categoryRepository, IMessageQueueService messageQueueService)
    {
        _categoryRepository = categoryRepository;
        _messageQueueService = messageQueueService;
    }

    public async Task<int> AddCategory(Category category)
    {
        _messageQueueService.PublishMessage("add-category-queue", JsonSerializer.Serialize(category));
        return await _categoryRepository.AddCategory(category);
    }

    public async Task<int> AddSubCategory(SubCategory subCategory, int categoryId)
    {
        _messageQueueService.PublishMessage("add-subcategory-queue", JsonSerializer.Serialize(new { subCategory, categoryId }));

        return await _categoryRepository.AddSubCategory(subCategory, categoryId);
    }

    public async Task<List<CategoryDto>> GetAllCategories()
    {
        return await _categoryRepository.GetAllCategories();
    }

    public async Task<List<SubCategory>> GetAllSubCategories()
    {
        return await _categoryRepository.GetAllSubCategories();
    }

    public Task<Category> DeleteCategory(int id)
    {
        throw new NotImplementedException();
    }

    public Task<SubCategory> GetSubCategoryById(int categoryId)
    {
        throw new NotImplementedException();
    }

    public Task<SubCategory> GetSubCategoryByName(string subCategoryName)
    {
        throw new NotImplementedException();
    }

    public Task<Category> UpdateCategory(Category category)
    {
        throw new NotImplementedException();
    }



}

