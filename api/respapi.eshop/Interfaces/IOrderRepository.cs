using respapi.eshop.Models.Entities;

namespace respapi.eshop.Interfaces;
public interface IOrderRepository
{
    Task<Order> CreateOrder(Order order, string username);
}

