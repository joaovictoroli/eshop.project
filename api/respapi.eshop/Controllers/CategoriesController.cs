using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using respapi.eshop.Interfaces;
using respapi.eshop.Models.DTOs;
using respapi.eshop.Models.Entities;

namespace respapi.eshop.Controllers;
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
        string gotAdded = await _categoryRepository.AddCategory(category);
        if (gotAdded != "Added") { return BadRequest(gotAdded); }
        return Ok(addCategoryDto);
    }

    [Authorize(Policy = "RequireAdminRole")]
    [HttpPost("add-subcategory")]
    public async Task<ActionResult<SubCategoryDto>> AddSubCategory(SubCategoryDto subCategoryDto)
    {
        var subCategory = _mapper.Map<SubCategory>(subCategoryDto);
        string gotAdded = await _categoryRepository.AddSubCategory(subCategory, subCategoryDto.CategoryId);

        if (gotAdded != "Added") { return BadRequest(gotAdded); }

        return Ok(subCategoryDto);
    }

    [Authorize(Policy = "RequireAdminRole")]
    [HttpDelete("delete-subcategory/{subCategoryId}")]
    public async Task<ActionResult> DeleteSubCategory(int subCategoryId)
    {
        string result = await _categoryRepository.DeleteSubCategory(subCategoryId);
        if (result != "Deleted") { return BadRequest(result); }
        return NoContent();
    }

    [Authorize(Policy = "RequireAdminRole")]
    [HttpDelete("delete-category/{categoryId}")]
    public async Task<ActionResult> DeleteCategory(int categoryId)
    {
        string result = await _categoryRepository.DeleteCategory(categoryId);
        if (result != "Deleted") { return BadRequest(result); }
        return NoContent();
    }
}