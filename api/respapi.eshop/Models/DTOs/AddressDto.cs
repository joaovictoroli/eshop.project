namespace respapi.eshop.Models.DTOs
{
    public class AddressDto
    {
        public int? Id { get; set; }
        public string? Cep { get; set; }
        public string? Uf { get; set; }
        public string? Bairro { get; set; }
        public string? Complemento { get; set; }
        public int? Numero { get; set; }
        public int? Apartamento { get; set; }
        public string? InfoAdicinal { get; set; }
        public bool? IsMain { get; set; }
    }
}
