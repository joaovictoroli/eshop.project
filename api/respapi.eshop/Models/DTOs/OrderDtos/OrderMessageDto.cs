using respapi.eshop.Models.Entities;

namespace respapi.eshop.Models.DTOs.OrderDtos;
public class OrderMessageDto
{
    public Order Order { get; set; }
    public string Username { get; set; }
}

