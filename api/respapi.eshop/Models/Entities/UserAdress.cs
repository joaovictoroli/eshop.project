﻿using Microsoft.EntityFrameworkCore;

namespace respapi.eshop.Models.Entities
{
    public class UserAdress
    {
        public int Id { get; set; }
        public string Cep { get; set; }
        public string Uf { get; set; }
        public string Bairro { get; set; }
        public string Complemento { get; set; }
        public int Numero { get; set; }
        public int Apartamento { get; set; }
        public string InfoAdicinal { get; set; }
        public bool IsMain { get; set; }
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
