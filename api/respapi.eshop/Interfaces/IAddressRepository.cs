using respapi.eshop.Models.Entities;

namespace respapi.eshop.Interfaces
{
    public interface IAddressRepository
    {
        Task<UserAddress?> GetUserAddressById(int id);
        Task<UserAddress> AddUserAdress(UserAddress userAdress);
        Task<bool> DeleteUserAddress(UserAddress userAdress);
        Task<bool?> ChangeMainAddress(UserAddress currentMain, UserAddress nextMain);
    }
}
