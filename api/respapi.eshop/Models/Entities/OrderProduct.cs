using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace respapi.eshop.Models.Entities;
public class OrderProduct
{
    [Required]
    [ForeignKey("Order")]
    public int? OrderId { get; set; }
    public virtual Order Order { get; set; }

    [Required]
    [ForeignKey("Product")]
    public int? ProductId { get; set; }
    public virtual Product Product { get; set; }
    [Required]
    public int? Quantity { get; set; }
    [Required]
    public decimal? Price { get; set; }
    [Required]
    public string? ProductName { get; set; }
    [Required]
    public string? ProductImageUrl { get; set; }
}


