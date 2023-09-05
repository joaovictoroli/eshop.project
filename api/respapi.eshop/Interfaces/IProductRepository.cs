using respapi.eshop.Helpers;
using respapi.eshop.Models.DTOs;
using respapi.eshop.Models.Entities;

namespace respapi.eshop.Interfaces
{
    public interface IProductRepository
    {
        Task<PagedList<Product>> GetAllProducts(UserParams userParams);
        Task<Product> AddProduct(Product product);
        Task<Product> GetProductById(int id);
    }
}
