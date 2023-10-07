using System.ComponentModel.DataAnnotations;
using respapi.eshop.Models.DTOs.OrderDtos;

namespace respapi.eshop.Models.DTOs;
public class OrderDto
{
    public int? OrderId { get; set;}
    public int? UserId { get; set; }
    public ICollection<OrderProductDto>? Products { get; set; }
    public OrderAddressDto? OrderAddress { get; set; }
    public DateTime SubmittedAt { get; set; }
    public decimal TotalPrice { get; set; }
}

