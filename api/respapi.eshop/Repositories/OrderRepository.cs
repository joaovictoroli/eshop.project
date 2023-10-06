using System.ComponentModel;
using System;
using respapi.eshop.Interfaces;
using respapi.eshop.Models.Entities;
using respapi.eshop.Data;

namespace respapi.eshop.Repositories;
public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _dbContext;

    private readonly IUserDetailCacheService _userDetailCacheService;
    public OrderRepository(AppDbContext appDbContext, IUserDetailCacheService userDetailCacheService)
    {
        _dbContext = appDbContext;
        _userDetailCacheService = userDetailCacheService;
    }

    public async Task<Order> CreateOrder(Order order, string username)
    {
        Console.WriteLine("Enviado para o banco de dados...");
        await _dbContext.Orders.AddAsync(order);
        var isSaved = await _dbContext.SaveChangesAsync();

        if (isSaved > 0)
        {
            await _userDetailCacheService.RemoveAsync($"user:{username}");
        }
        return order;
    }

}

