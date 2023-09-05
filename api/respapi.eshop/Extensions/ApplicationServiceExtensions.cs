using Microsoft.EntityFrameworkCore;
using respapi.eshop.Data;
using respapi.eshop.Interfaces;
using respapi.eshop.Repositories;
using respapi.eshop.Services;

namespace respapi.eshop.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
            IConfiguration config)
        {
            services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseSqlServer(config.GetConnectionString("DatabaseConnection"));
            });
            services.AddCors();
            services.AddHttpContextAccessor();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ICepService, CepService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IImageRepository, ImageRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            return services;
        }
    }
}
