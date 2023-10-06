using System;

namespace respapi.eshop.Models.DTOs.OrderDtos;
public class OrderProductDto
{
    public int? Quantity { get; set; }
    public decimal? Price { get; set; }
    public string? ProductName { get; set; }
    public string? ProductImageUrl { get; set; }
}

