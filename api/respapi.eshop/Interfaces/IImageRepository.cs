using respapi.eshop.Models.Entities;

namespace respapi.eshop.Interfaces
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
    }
}
