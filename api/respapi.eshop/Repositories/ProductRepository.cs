using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using respapi.eshop.Data;
using respapi.eshop.Helpers;
using respapi.eshop.Interfaces;
using respapi.eshop.Models.DTOs;
using respapi.eshop.Models.Entities;
using System.Linq;
using System.Reflection;

namespace respapi.eshop.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _dbContext;

        public ProductRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> AddProduct(Product product)
        {
            _dbContext.Add(product);
            int gotAdded = await _dbContext.SaveChangesAsync();
            return gotAdded;
        }

        public async Task<PagedList<Product>> GetAllProducts(UserParams userParams)
        {
            var query = _dbContext.Products.AsQueryable();

            if (!userParams.Name.IsNullOrEmpty())
            {
                query = query.Where(x => x.Name.Contains(userParams.Name));
            }

            if (userParams.MinPrice != 0)
            {
                query = query.Where(x => x.Price > userParams.MinPrice);
            }

            if (userParams.MaxPrice != 9999)
            {
                query = query.Where(x=> x.Price < userParams.MaxPrice);
            }

            if (!userParams.SubCategoryName.IsNullOrEmpty())
            {
                var subCategory = await  _dbContext.SubCategories.FirstOrDefaultAsync(x => x.Name == userParams.SubCategoryName);
                if (subCategory != null)
                {
                    query = query.Where(x => x.SubCategoryId == subCategory.Id);
                }
            }

            query = query.OrderByDescending(x => x.Price);

            return await PagedList<Product>.CreateAsync(query.AsNoTracking(),
               userParams.PageNumber, userParams.PageSize);
        }

        
        public async Task<Product> GetProductById(int productId)
        {
            return await _dbContext.Products.SingleOrDefaultAsync(x => x.Id == productId);
        }

        public async Task<Product> GetProductByName(string productName)
        {
            return await _dbContext.Products.SingleOrDefaultAsync(x => x.Name == productName);
        }

        public async Task<int> DeleteProductById(int productId)
        {
            int deleted = 0;
            var product = await _dbContext.Products.SingleOrDefaultAsync(x => x.Id == productId);
            if (product != null)
            {
                _dbContext.Products.Remove(product);
                deleted = await _dbContext.SaveChangesAsync();
            }
            return deleted;
        }
    }
}
