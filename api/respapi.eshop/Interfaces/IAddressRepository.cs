using respapi.eshop.Models.Entities;

namespace respapi.eshop.Interfaces
{
    public interface IAddressRepository
    {
        Task<UserAdress> AddUserAdress(UserAdress userAdress);
    }
}
