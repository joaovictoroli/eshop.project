using System;
using respapi.eshop.Models.Entities;

namespace respapi.eshop.Interfaces;
public interface IOrderRepository
{
    Task<Order> CreateOrder(Order order);
    // Task<Order> GetOrderById(int id);
    // Task<List<Order>> GetOrders();
    // Task<Order> UpdateOrder(Order order);
    // Task<Order> DeleteOrder(int id);
}

