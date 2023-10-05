using System.ComponentModel;
using System;
using respapi.eshop.Interfaces;
using respapi.eshop.Models.Entities;
using respapi.eshop.Data;

namespace respapi.eshop.Repositories;
public class OrderRepository : IOrderRepository
{
     private readonly AppDbContext _dbContext;
    public OrderRepository(AppDbContext appDbContext)
    {
        _dbContext = appDbContext;
    }

    public async Task<Order> CreateOrder(Order order)
    {
        await _dbContext.Orders.AddAsync(order);
        await _dbContext.SaveChangesAsync();
        return order;
    }

}

