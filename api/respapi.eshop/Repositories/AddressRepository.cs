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
    }
}
