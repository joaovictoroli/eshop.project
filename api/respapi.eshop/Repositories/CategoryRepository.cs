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

        public async Task<string> AddCategory(Category category)
        {
            int duplicated = await CheckDuplicate(category.Name);
            if (duplicated == 1) {
                return "category name is duplicated";
            }            
            _dbContext.Categories.Add(category);
            var gotAdded = await _dbContext.SaveChangesAsync();
            if (gotAdded == 0) { return "Something went wrong."; }
            return "Added";
        }

        public async Task<string> AddSubCategory(SubCategory subCategory, int categoryId)
        {
            int gotAdded = 0;
            var category = await _dbContext.Categories.FirstOrDefaultAsync(x => x.Id == categoryId);
            var duplicated = await CheckDuplicateSubCategory(subCategory.Name);

            if (duplicated == 1) {
                return "subcategory name is duplicated";
            }
            if (category == null)
            {
                return "category not found";
            } 
            subCategory.Category = category;        
            _dbContext.SubCategories.Add(subCategory);
            gotAdded = await _dbContext.SaveChangesAsync();
            if (gotAdded == 0) { return "Something went wrong."; }
            return "Added";
        }

        public async Task<string> DeleteCategory(int id)
        {
            int gotDeleted = 0;
            var category = await _dbContext.Categories
                            .Include(c => c.SubCategories)
                            .ThenInclude(sc => sc.Products)
                            .FirstOrDefaultAsync(x => x.Id == id);
            
            if (category == null) return "Category not found";
            if (category.SubCategories != null && category.SubCategories.Any())
            {
                if (category.SubCategories.Any(sc => sc.Products != null && sc.Products.Any()))
                {
                    return "There are products linked to this category.";
                }
                return "There are subcategories linked to this category";
            }

            if (category != null)
            {
                try {
                    _dbContext.Categories.Remove(category);
                    gotDeleted = await _dbContext.SaveChangesAsync();    
                } catch (Exception ex) {
                    return "Something went wrong";
                }              
            }
            return "Deleted";
        }

        public async Task<string> DeleteSubCategory(int id)
        {
            int gotDeleted = 0;
            var subCategory = await _dbContext.SubCategories
                                        .Include(c => c.Products)
                                        .FirstOrDefaultAsync(x => x.Id == id);
            if (subCategory == null) return "SubCategory not found";

            if (subCategory.Products != null && subCategory.Products.Any())
            {
                return "There are products linked to this subcategory.";
            }

            if (subCategory != null)
            {
                try {
                    _dbContext.SubCategories.Remove(subCategory);
                gotDeleted = await _dbContext.SaveChangesAsync();       
                } catch (Exception ex) {
                    return "Something went wrong";
                }            
            }
            return "Deleted";
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

        public async Task<SubCategory> GetSubCategoryByName(string subCategoryName)
        {
            var subCategory = await _dbContext.SubCategories.FirstOrDefaultAsync(x=> x.Name == subCategoryName);
            return subCategory;
        }

        private async Task<int> CheckDuplicate(string categoryName) {
            var category = await _dbContext.Categories.FirstOrDefaultAsync(
                                            x => x.Name.ToLower() == categoryName.ToLower());
            if (category != null)
            {
                return 1;
            } else {
                return 0;
            }
        }
        private async Task<int> CheckDuplicateSubCategory(string subCategoryName) {
            var subCategory = await _dbContext.SubCategories.FirstOrDefaultAsync(
                                    x => x.Name.ToLower() == subCategoryName.ToLower());

            if (subCategory != null)
            {
                return 1;
            } else {
                return 0;
            }
        }
    }
}
