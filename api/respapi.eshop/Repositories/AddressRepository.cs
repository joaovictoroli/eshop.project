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

        public AddressRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<UserAdress> AddUserAdress(UserAdress userAdress)
        {
            await _dbContext.UserAdresses.AddAsync(userAdress);
            await _dbContext.SaveChangesAsync();
            return userAdress;
        }

        public async Task<bool?> ChangeMainAddress(UserAdress currentMain, UserAdress nextMain)
        {
             var previousMain = await (from p in _dbContext.UserAdresses
                                where p == currentMain select p)
                                .SingleOrDefaultAsync();

            if (previousMain == null) { return false; }

            previousMain.IsMain = false;


            var newMain = await (from p in _dbContext.UserAdresses
                                  where p == nextMain
                                  select p)
                                .SingleOrDefaultAsync();

            if (newMain == null) { return false; }
            
            newMain.IsMain = true;
            return await SaveChanges();
        }

        public async Task<bool> DeleteUserAddress(UserAdress userAdress)
        {
            _dbContext.UserAdresses.Remove(userAdress);
            var isDeleted = await _dbContext.SaveChangesAsync();

            return await SaveChanges();
        }

        public async Task<UserAdress?> GetUserAddressById(int id)
        {
            return await _dbContext.UserAdresses.FindAsync(id);
        }

        private async Task<bool> SaveChanges()
        {
            var isSaved = await _dbContext.SaveChangesAsync();
            if (isSaved == 0) { return false; }
            return true;
        }
    }
}
