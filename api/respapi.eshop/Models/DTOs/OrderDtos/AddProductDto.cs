using System;
using System.ComponentModel.DataAnnotations;

namespace respapi.eshop.Models.DTOs.OrderDtos;
public class AddProductDto
{
        

    [Required]
    [MinLength(4, ErrorMessage = "The length of {0} must be at least {1} characters.")]
    [MaxLength(250, ErrorMessage = "The length of {0} cannot be more than {1} characters.")]
    public string? Name { get; set; }
    [Required]
    [MinLength(4, ErrorMessage = "The length of {0} must be at least {1} characters.")]
    public string? Description { get; set; }     

    [Required(ErrorMessage = "Price is required")]
    public float? Price { get; set; }
    [Required]
    public string? TechnicalInfo { get; set; }
    public string? ImageUrl { get; set; }
    public int? SubCategoryId { get; set; }
    public string? SubCategoryName { get; set; } = string.Empty;    
}
