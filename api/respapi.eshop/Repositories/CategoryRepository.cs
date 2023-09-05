using AutoMapper;
using Microsoft.EntityFrameworkCore;
using respapi.eshop.Data;
using respapi.eshop.Interfaces;
using respapi.eshop.Models.DTOs;
using respapi.eshop.Models.Entities;

namespace respapi.eshop.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public CategoryRepository(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Category> AddCategory(Category category)
        {
            _dbContext.Categories.Add(category);
            await _dbContext.SaveChangesAsync();
            return category;
        }

        public async Task<SubCategory> AddSubCategory(SubCategory subCategory, int categoryId)
        {            
            var category = await _dbContext.Categories.FirstOrDefaultAsync(x => x.Id == categoryId);
            if (category != null)
            {
                subCategory.Category = category;                
                _dbContext.SubCategories.Add(subCategory);
                await _dbContext.SaveChangesAsync();
                return subCategory;
            }
            return subCategory;
        }

        public Task<Category> DeleteCategory(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CategoryDto>> GetAllCategories()
        {
            var categories = await _dbContext.Categories.Include(x => x.SubCategories).ToListAsync();
            return _mapper.Map<List<CategoryDto>>(categories);
        }

        public async Task<List<SubCategory>> GetAllSubCategories()
        {
            return await _dbContext.SubCategories.ToListAsync();
        }

        public Task<SubCategory> GetSubCategoryById(int categoryId)
        {
            throw new NotImplementedException();
        }

        public async Task<SubCategory> GetSubCategoryByName(string subCategoryName)
        {
            var subCategory = await _dbContext.SubCategories.FirstOrDefaultAsync(x=> x.Name == subCategoryName);
            return subCategory;
        }

        public Task<Category> UpdateCategory(Category category)
        {
            throw new NotImplementedException();
        }
    }
}
