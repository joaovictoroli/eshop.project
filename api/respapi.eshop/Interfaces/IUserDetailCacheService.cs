namespace respapi.eshop.Interfaces;
public interface IUserDetailCacheService
{
    Task<T> GetOrCreateAsync<T>(string cacheKey, Func<Task<T>> factoryMethod, TimeSpan? expiry = null);
    Task RemoveAsync(string cacheKey);
}

