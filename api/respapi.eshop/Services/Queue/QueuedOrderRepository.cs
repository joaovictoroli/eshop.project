using System;
using System.Text.Json;
using respapi.eshop.Interfaces;
using respapi.eshop.Models.DTOs.OrderDtos;
using respapi.eshop.Models.Entities;

namespace respapi.eshop.Services.Queue;
public class QueuedOrderRepository : IQueuedOrderRepository
{
    private readonly IMessageQueueService _messageQueueService;

    public QueuedOrderRepository(IMessageQueueService messageQueueService)
    {
        _messageQueueService = messageQueueService;
    }

    public async Task<Order> CreateOrder(Order order, string username)
    {
        try
        {
            var orderMessage = new OrderMessageDto 
            {
                Order = order,
                Username = username
            };
            Console.WriteLine("Inserting order into queue...");
            await _messageQueueService.PublishMessage("create-order-queue", JsonSerializer.Serialize(orderMessage));
            return order;
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Error occurred while queueing the order: {ex.Message}. StackTrace: {ex.StackTrace}");
            throw;
        }
    }
}
