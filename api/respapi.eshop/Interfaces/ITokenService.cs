using respapi.eshop.Models.Entities;

namespace respapi.eshop.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(AppUser user);
    }
}
