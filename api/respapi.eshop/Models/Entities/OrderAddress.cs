using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace respapi.eshop.Models.Entities;
public class OrderAddress
{
    public string Cep { get; set; }  
    public string Uf { get; set; }
    public string Bairro { get; set; }
    public string Complemento { get; set; }
    public int Numero { get; set; }
    public int Apartamento { get; set; }
    public string InfoAdicinal { get; set; }
    
    [Key, ForeignKey("Order")]
    public int OrderId { get; set; }
    public Order Order { get; set; }
}

