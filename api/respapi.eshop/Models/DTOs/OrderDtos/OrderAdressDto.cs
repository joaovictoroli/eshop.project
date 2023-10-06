using System.ComponentModel.DataAnnotations;

namespace respapi.eshop.Models.DTOs;
public class OrderAddressDto
{
    public string? Cep { get; set; }   
    public string? Uf { get; set; }
    public string? Bairro { get; set; }
    public string? Complemento { get; set; }
    public int? Numero { get; set; }
    public int? Apartamento { get; set; }
    public string? InfoAdicinal { get; set; }
}

