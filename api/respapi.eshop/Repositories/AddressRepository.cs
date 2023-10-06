using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using respapi.eshop.Data;
using respapi.eshop.Interfaces;
using respapi.eshop.Models.Entities;

namespace respapi.eshop.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly AppDbContext _dbContext;

        private readonly IUserDetailCacheService _userDetailCacheService;

        public AddressRepository(AppDbContext dbContext, IUserDetailCacheService userDetailCacheService)
        {
            _dbContext = dbContext;
            _userDetailCacheService = userDetailCacheService;
        }
        public async Task<UserAddress> AddUserAdress(UserAddress userAdress, string username)
        {
            await _dbContext.UserAddresses.AddAsync(userAdress);
            var isSaved = await _dbContext.SaveChangesAsync();
            if (isSaved > 0)
            {
                await _userDetailCacheService.RemoveAsync($"user:{username}");
            }
            return userAdress;
        }

        public async Task<bool?> ChangeMainAddress(UserAddress currentMain, UserAddress nextMain, string username)
        {
             var previousMain = await (from p in _dbContext.UserAddresses
                                where p == currentMain select p)
                                .SingleOrDefaultAsync();

            if (previousMain == null) { return false; }

            previousMain.IsMain = false;


            var newMain = await (from p in _dbContext.UserAddresses
                                  where p == nextMain
                                  select p)
                                .SingleOrDefaultAsync();

            if (newMain == null) { return false; }
            
            newMain.IsMain = true;
            var isSaved = await SaveChanges();

            if (isSaved)
            {
                await _userDetailCacheService.RemoveAsync($"user:{username}");
            }
            return isSaved;
        }

        public async Task<bool> DeleteUserAddress(UserAddress userAdress, string username)
        {
            _dbContext.UserAddresses.Remove(userAdress);
            bool isSaved = await SaveChanges();
            
            if (isSaved)
            {
                await _userDetailCacheService.RemoveAsync($"user:{username}");
            }

            return isSaved;        
        }

        public async Task<UserAddress?> GetUserAddressById(int id)
        {
            return await _dbContext.UserAddresses.FindAsync(id);
        }

        private async Task<bool> SaveChanges()
        {
            var isSaved = await _dbContext.SaveChangesAsync();
            if (isSaved == 0) { return false; }
            return true;
        }
    }
}
