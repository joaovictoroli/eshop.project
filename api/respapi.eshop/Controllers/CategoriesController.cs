using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using respapi.eshop.Interfaces;
using respapi.eshop.Models.DTOs;
using respapi.eshop.Models.Entities;

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

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost("add-category")]
        public async Task<ActionResult<AddCategoryDto>> AddCategory(AddCategoryDto addCategoryDto)
        {
            var category = _mapper.Map<Category>(addCategoryDto);
            int gotAdded = await _categoryRepository.AddCategory(category);
            if (gotAdded == 0) { return BadRequest("Something went wrong."); }
            return Ok(addCategoryDto);
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost("add-subcategory")]
        public async Task<ActionResult<SubCategoryDto>> AddSubCategory(SubCategoryDto subCategoryDto)
        {
            var subCategory = _mapper.Map<SubCategory>(subCategoryDto);
            int gotAdded = await _categoryRepository.AddSubCategory(subCategory, subCategoryDto.CategoryId);
            if (gotAdded == 0) { return BadRequest("Something went wrong."); }
            return Ok(subCategoryDto);
        }
    }
}
