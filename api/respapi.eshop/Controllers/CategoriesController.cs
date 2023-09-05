using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using respapi.eshop.Interfaces;
using respapi.eshop.Models.DTOs;
using respapi.eshop.Models.Entities;
using respapi.eshop.Repositories;

namespace respapi.eshop.Controllers
{
    public class CategoriesController : BaseApiController
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;


        public CategoriesController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoryDto>>> GetAllCategoriesNSubCategories()
        {
            var categories = await _categoryRepository.GetAllCategories();
            return Ok(categories);
        }

        [HttpPost("add-category")]
        public async Task<ActionResult<AddCategoryDto>> AddCategory(AddCategoryDto addCategoryDto)
        {
            var category = _mapper.Map<Category>(addCategoryDto);
            await _categoryRepository.AddCategory(category);
            return Ok(addCategoryDto);
        }

        [HttpPost("add-subcategory")]
        public async Task<ActionResult<SubCategoryDto>> AddSubCategory(SubCategoryDto subCategoryDto)
        {
            var subCategory = _mapper.Map<SubCategory>(subCategoryDto);
            await _categoryRepository.AddSubCategory(subCategory, subCategoryDto.CategoryId);
            return Ok(subCategory);
        }
    }
}
