using System.ComponentModel.DataAnnotations;
using respapi.eshop.Models.DTOs.OrderDtos;
using respapi.eshop.Models.Entities;

namespace respapi.eshop.Models.DTOs;
public class AddOrderDto
{
    public List<AddOrderProduct>? OrderProducts { get; set; }
}

