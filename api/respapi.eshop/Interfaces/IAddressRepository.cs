using respapi.eshop.Models.Entities;

namespace respapi.eshop.Interfaces
{
    public interface IAddressRepository
    {
        Task<UserAdress> GetUserAddressById(int id);
        Task<UserAdress> AddUserAdress(UserAdress userAdress);
        Task<bool> DeleteUserAddress(UserAdress userAdress);
        Task<bool?> ChangeMainAddress(UserAdress currentMain, UserAdress nextMain);
    }
}
