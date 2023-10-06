using respapi.eshop.Models.Entities;

namespace respapi.eshop.Interfaces
{
    public interface IAddressRepository
    {
        Task<UserAddress?> GetUserAddressById(int id);
        Task<UserAddress> AddUserAdress(UserAddress userAdress, string username);
        Task<bool> DeleteUserAddress(UserAddress userAdress, string username);
        Task<bool?> ChangeMainAddress(UserAddress currentMain, UserAddress nextMain, string username);
    }
}
