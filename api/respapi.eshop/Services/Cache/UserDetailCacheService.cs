using System.Text.Json;
using respapi.eshop.Interfaces;
using StackExchange.Redis;

namespace respapi.eshop.Services.Cache;
public class UserDetailCacheService : IUserDetailCacheService
{
    private readonly IDatabase _cache;

    public UserDetailCacheService(IDatabase cache)
    {
        _cache = cache;
    }

    public async Task<T> GetOrCreateAsync<T>(string cacheKey, Func<Task<T>> factoryMethod, TimeSpan? expiry = null)
    {
        var cachedData = _cache.StringGet(cacheKey);
        if (!cachedData.IsNullOrEmpty)
        {
            return JsonSerializer.Deserialize<T>(cachedData);
        }

        var data = await factoryMethod();
        _cache.StringSet(cacheKey, JsonSerializer.Serialize(data), expiry);

        return data;
    }

    public async Task RemoveAsync(string cacheKey)
    {
        await _cache.KeyDeleteAsync(cacheKey);
    }
}

