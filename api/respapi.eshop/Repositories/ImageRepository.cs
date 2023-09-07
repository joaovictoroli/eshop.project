using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using respapi.eshop.Data;
using respapi.eshop.Interfaces;
using respapi.eshop.Models.Entities;


namespace respapi.eshop.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AppDbContext _dbContext;

        public ImageRepository(
            IWebHostEnvironment webHostEnvironment,
            IHttpContextAccessor httpContextAccessor,
            AppDbContext dbContext)
        {          
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
            _dbContext = dbContext;
        }

        public async Task<bool> CheckDuplicate(string fileName)
        {
            var list = await _dbContext.Images.Where(x => x.FileName == fileName).ToListAsync();
            if (list.IsNullOrEmpty()) { return false; }
            return true;

        }

        public async Task<int> DeleteImage(string imageUrl)
        {
            int gotDeleted = 0;
            var image = await _dbContext.Images.FirstOrDefaultAsync(x => x.FilePath == imageUrl);

            if (image == null) { return gotDeleted; }
            var localFilePath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images",
                $"{image.FileName}{image.FileExtension}");

            if (File.Exists(localFilePath))
            {
                File.Delete(localFilePath);
                _dbContext.Images.Remove(image);
                gotDeleted = await _dbContext.SaveChangesAsync();
            }
            return gotDeleted;
        }

        public async Task<Image> Upload(Image image)
        {
            var localFilePath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images",
                $"{image.FileName}{image.FileExtension}");

            
            using var stream = new FileStream(localFilePath, FileMode.Create);
            await image.File.CopyToAsync(stream);

            var urlFilePath = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}{_httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtension}";
            image.FilePath = urlFilePath;

            await _dbContext.Images.AddAsync(image);
            await _dbContext.SaveChangesAsync();

            return image;
        }

        public string GetImageUrl(string fileName)
        {
            var urlFilePath = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}{_httpContextAccessor.HttpContext.Request.PathBase}/Images/{fileName}";

            return urlFilePath;
        }

        public string GetImagePath(string fileName)
        {
            var localFilePath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images",
               $"{fileName}");

            return localFilePath;
        }
    }
}
