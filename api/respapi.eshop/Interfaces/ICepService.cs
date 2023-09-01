using respapi.eshop.Models;
using respapi.eshop.Models.Entities;

namespace respapi.eshop.Interfaces
{
    public interface ICepService
    {
        Task<CepApiResponse> GetAdressByCep(string cep);
    }
}
