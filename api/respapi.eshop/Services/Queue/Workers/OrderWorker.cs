using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;
using respapi.eshop.Interfaces;
using System.Text.Json;
using respapi.eshop.Models.Entities;
using respapi.eshop.Models.DTOs.OrderDtos;

namespace respapi.eshop.Services.Queue.Workers;

public class OrderWorker : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public OrderWorker(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
       
    }

    private void Initialize()
    {
        var _messageQueueService = _serviceProvider.GetRequiredService<IMessageQueueService>();

        _messageQueueService.Subscribe("create-order-queue", message => 
        {
            _ = ProcessOrder(message);
        });
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        Initialize();
        await Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
    private async Task ProcessOrder(string message) 
    {
        var orderMessage = JsonSerializer.Deserialize<OrderMessageDto>(message);
        if (orderMessage  is not null)
        {
            if (orderMessage != null && orderMessage.Order is not null)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    Console.WriteLine("Sending request to db...");
                    Console.WriteLine($"Processed order to user with ID: {orderMessage.Order.UserId}");
                    var _orderRepository = scope.ServiceProvider.GetRequiredService<IOrderRepository>();          
                    await _orderRepository.CreateOrder(orderMessage.Order, orderMessage.Username);
                }
            }
        }
    }
}

