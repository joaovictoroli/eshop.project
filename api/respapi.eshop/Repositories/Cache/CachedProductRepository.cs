using System.Text.Json;
using respapi.eshop.Helpers;
using respapi.eshop.Interfaces;
using respapi.eshop.Models.Entities;
using StackExchange.Redis;

namespace respapi.eshop.Repositories.Cache;public class CachedProductRepository : IProductRepository
{
    private readonly IProductRepository _productRepository;
    private readonly IDatabase _cache;

    public CachedProductRepository(IProductRepository productRepository, IDatabase cache)
    {
        _productRepository = productRepository;
        _cache = cache;
    }

    public async Task<int> AddProduct(Product product)
    {
        var result = await _productRepository.AddProduct(product);

        _cache.KeyDelete("all-products");

        return result;
    }
    public async Task<PagedList<Product>> GetAllProducts(UserParams userParams)
    {
        var cacheKey = $"all-products:{userParams.Name}:{userParams.MinPrice}:{userParams.MaxPrice}:{userParams.SubCategoryName}:{userParams.PageNumber}:{userParams.PageSize}";

        var cachedProducts = _cache.StringGet(cacheKey);
        if (!cachedProducts.IsNullOrEmpty)
        {
            // Usando o conversor diretamente aqui
            return JsonSerializer.Deserialize<PagedList<Product>>(cachedProducts, new JsonSerializerOptions
            {
                Converters = { new PagedListConverter<Product>() }
            });
        }

        var products = await _productRepository.GetAllProducts(userParams);
        _cache.StringSet(cacheKey, JsonSerializer.Serialize(products, new JsonSerializerOptions
        {
            Converters = { new PagedListConverter<Product>() }
        }), TimeSpan.FromHours(1));

        return products;
    }
    public async Task<Product> GetProductById(int productId)
    {
        var cacheKey = $"product-id:{productId}";

        var cachedProduct = _cache.StringGet(cacheKey);
        if (!cachedProduct.IsNullOrEmpty)
        {
            return JsonSerializer.Deserialize<Product>(cachedProduct);
        }

        var product = await _productRepository.GetProductById(productId);
        _cache.StringSet(cacheKey, JsonSerializer.Serialize(product), TimeSpan.FromHours(1));

        return product;
    }

    public async Task<Product> GetProductByName(string productName)
    {
        var cacheKey = $"product-name:{productName}";

        var cachedProduct = _cache.StringGet(cacheKey);
        if (!cachedProduct.IsNullOrEmpty)
        {
            return JsonSerializer.Deserialize<Product>(cachedProduct);
        }

        var product = await _productRepository.GetProductByName(productName);
        _cache.StringSet(cacheKey, JsonSerializer.Serialize(product), TimeSpan.FromHours(1));

        return product;
    }

    public async Task<int> DeleteProductById(int productId)
    {
        var result = await _productRepository.DeleteProductById(productId);
        _cache.KeyDelete($"product-id:{productId}");

        _cache.KeyDelete("all-products");

        return result;
    }
}
