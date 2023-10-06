using AutoMapper;
using Microsoft.EntityFrameworkCore;
using respapi.eshop.Data;
using respapi.eshop.Interfaces;
using respapi.eshop.Models.Entities;

namespace respapi.eshop.Repositories;
public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    private readonly IUserDetailCacheService _userDetailCacheService;

    public UserRepository(AppDbContext context, IMapper mapper, IUserDetailCacheService userDetailCacheService)
    {
        _context = context;
        _mapper = mapper;
        _userDetailCacheService = userDetailCacheService;
    }

    public async Task<AppUser?> GetUserByUsernameAsync(string username)
    {
        return await GetOrSetCache($"user:{username}", async () =>
        {
            return await _context.Users
                .Include(p => p.Addresses)
                .Include(p => p.Orders)
                    .ThenInclude(o => o.OrderAddress)
                .Include(p => p.Orders)
                    .ThenInclude(o => o.Products)
                .Where(x => x.UserName == username).AsSplitQuery().FirstOrDefaultAsync();
        });
    }

        // return await _context.Users
        //         .Include(p => p.Addresses)
        //         .Include(p => p.Orders)
        //             .ThenInclude(o => o.OrderAddress)
        //         .Include(p => p.Orders)
        //             .ThenInclude(o => o.Products)
        //         .Where(x =>x.UserName == username).AsSplitQuery().FirstOrDefaultAsync();
  

    private async Task<T> GetOrSetCache<T>(string cacheKey, Func<Task<T>> retrievalFunc)
    {
        return await _userDetailCacheService.GetOrCreateAsync(cacheKey, retrievalFunc, TimeSpan.FromHours(1));
    }
}

