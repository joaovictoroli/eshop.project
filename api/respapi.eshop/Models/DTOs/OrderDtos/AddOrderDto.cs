using System.ComponentModel.DataAnnotations;
using respapi.eshop.Models.DTOs.OrderDtos;
using respapi.eshop.Models.Entities;

namespace respapi.eshop.Models.DTOs;
public class AddOrderDto
{
    // public int Id { get; set; }
    // public int? UserId { get; set; }

    // -> OrderProduct 2fk and quantity and priceunit instead of list<products>
    [Required]
    public List<AddOrderProduct>? OrderProducts { get; set; }
}

