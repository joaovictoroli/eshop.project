using System.Text.Json;
using respapi.eshop.Helpers;
using respapi.eshop.Interfaces;
using respapi.eshop.Models.Entities;

namespace respapi.eshop.Services.Queue;
public class QueuedProductRepository : IProductRepository
{
    private readonly IProductRepository _productRepository;
    private readonly IMessageQueueService _messageQueueService;

    public QueuedProductRepository(IProductRepository productRepository, IMessageQueueService messageQueueService)
    {
        _productRepository = productRepository;
        _messageQueueService = messageQueueService;
    }

    public async Task<int> AddProduct(Product product)
    {
        await _messageQueueService.PublishMessage("add-product-queue", JsonSerializer.Serialize(product));
        return await _productRepository.AddProduct(product);
    }

    public async Task<PagedList<Product>> GetAllProducts(UserParams userParams)
    {
        return await _productRepository.GetAllProducts(userParams);
    }

    public async Task<Product> GetProductById(int productId)
    {
        return await _productRepository.GetProductById(productId);
    }

    public async Task<Product> GetProductByName(string productName)
    {
        return await _productRepository.GetProductByName(productName);
    }

    public async Task<int> DeleteProductById(int productId)
    {
        return await _productRepository.DeleteProductById(productId);
    }
}

