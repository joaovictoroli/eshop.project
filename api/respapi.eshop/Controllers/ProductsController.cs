using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using respapi.eshop.Extensions;
using respapi.eshop.Helpers;
using respapi.eshop.Interfaces;
using respapi.eshop.Models.DTOs;
using respapi.eshop.Models.DTOs.OrderDtos;
using respapi.eshop.Models.Entities;

namespace respapi.eshop.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IProductRepository _productRepository;
        private readonly IImageRepository _imageRepository;
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;

        public ProductsController(IProductRepository productRepository,
            IImageRepository imageRepository,
            IMapper mapper,
            ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _imageRepository = imageRepository;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductDto>>> GetAllProducts([FromQuery] UserParams userParams)
        {
            var products = await _productRepository.GetAllProducts(userParams);
            var productsDto = _mapper.Map<List<ProductDto>>(products);
            Response.AddPaginationHeader(new PaginationHeader(products.CurrentPage, products.PageSize,
                                        products.TotalCount, products.TotalPages));
            //if (productsDto.Count() == 0 ) { return Ok("No product has been found"); }
            return Ok(productsDto);
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost("add-product")]
        public async Task<ActionResult<ProductDto>> AddProduct([FromForm] AddProductDto productDto
        // , [FromForm] ImageUploadDto imageDto
        )
        {
            ValidateFileUpload(productDto.File);

            if (ModelState.IsValid)
            {
                var image = new Image
                {
                    File = productDto.File,
                    FileExtension = Path.GetExtension(productDto.File.FileName),
                    FileSizeInBytes = productDto.File.Length,
                    FileName = Path.GetFileNameWithoutExtension(productDto.File.FileName)
                };

                bool isDuplicate = await _imageRepository.CheckDuplicate(image.FileName);

                if (isDuplicate) { return BadRequest("Already has a file with this name"); }

                var persistedImage = await _imageRepository.Upload(image);
 
                var product = _mapper.Map<Product>(productDto);

                product.ImageUrl = persistedImage.FilePath;

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!productDto.SubCategoryName.IsNullOrEmpty())
                {
                    var subCategory = await _categoryRepository.GetSubCategoryByName(productDto.SubCategoryName);
                    if (subCategory != null) { product.SubCategoryId = subCategory.Id; }
                } else { return BadRequest("No SubCategory was found"); }

                await _productRepository.AddProduct(product);

                return Ok(productDto);
            }

            return BadRequest("Something went wrong");
        }

        [HttpGet("byName")]
        public async Task<ActionResult> GetProductByName(string productName)
        {
            var product = await _productRepository.GetProductByName(productName);

            if (product == null) { return NotFound("No product was found"); }

            var productDto = _mapper.Map<ProductDto>(product);
            return Ok(productDto);
        }

        [HttpGet("Id={productId}")]

        public async Task<ActionResult> GetProductById(int productId)
        {
            var product = await _productRepository.GetProductById(productId);

            if (product == null) { return NotFound("No product was found"); }

            var productDto = _mapper.Map<ProductDto>(product);
            return Ok(productDto);
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpDelete("delete-product/{productId}")]
        public async Task<ActionResult> DeleteProduct(int productId)
        {
            var product = await _productRepository.GetProductById(productId);

            var gotDeleted = await _imageRepository.DeleteImage(product.ImageUrl);

            if (gotDeleted == 0) { return NotFound("Something went wrong with ImageUrl"); }

            gotDeleted = await _productRepository.DeleteProductById(productId);

            if (gotDeleted == 0) { return NotFound("Something went wrong with Product."); }           

            return NoContent();
        }

        private void ValidateFileUpload(ImageUploadDto request)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };

            if (!allowedExtensions.Contains(Path.GetExtension(request.File.FileName)))
            {
                ModelState.AddModelError("file", "Unsupported file extension");
            }

            if (request.File.Length > 10485760)
            {
                ModelState.AddModelError("file", "File size more than 10MB, please upload a smaller size file.");
            }
        }

        private void ValidateFileUpload(IFormFile file)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };

            if (!allowedExtensions.Contains(Path.GetExtension(file.FileName)))
            {
                ModelState.AddModelError("file", "Unsupported file extension");
            }

            if (file.Length > 10485760)
            {
                ModelState.AddModelError("file", "File size more than 10MB, please upload a smaller size file.");
            }
        }
    }
}
