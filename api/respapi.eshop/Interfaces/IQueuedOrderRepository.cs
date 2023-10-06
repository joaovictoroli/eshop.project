using System;
using respapi.eshop.Models.Entities;

namespace respapi.eshop.Interfaces
{
    public interface IQueuedOrderRepository
    {
        Task<Order> CreateOrder(Order order, string username);  
    }
}
