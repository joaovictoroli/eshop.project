using respapi.eshop.Models.Entities;

namespace respapi.eshop.Interfaces
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
        Task<int> DeleteImage(string imageUrl);
        Task<bool> CheckDuplicate(string fileName);
    }
}
