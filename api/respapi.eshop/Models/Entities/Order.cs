using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace respapi.eshop.Models.Entities;
public class Order
{
    [Key]
    public int OrderId { get; set; }
    [JsonIgnore]
    public AppUser? AppUser { get; set; }
    public int UserId { get; set; }
    public ICollection<OrderProduct> Products { get; set; }
    public OrderAddress OrderAddress { get; set; }
    public int OrderAddressId { get; set; }
    public DateTime SubmittedAt { get; set; }
    public decimal TotalPrice { get; set; }   
}

