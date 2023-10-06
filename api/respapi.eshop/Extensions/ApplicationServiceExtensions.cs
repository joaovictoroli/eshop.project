using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using respapi.eshop.Data;
using respapi.eshop.Interfaces;
using respapi.eshop.Repositories;
using respapi.eshop.Repositories.Cache;
using respapi.eshop.Services;
using StackExchange.Redis;

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

            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });

            
            services.AddScoped<CategoryRepository>();
            services.AddSingleton(x => ConnectionMultiplexer.Connect("localhost:5555"));
            services.AddScoped<IDatabase>(x => x.GetRequiredService<ConnectionMultiplexer>().GetDatabase());
            services.AddScoped<ICategoryRepository>(serviceProvider =>
            {
                var originalRepository = serviceProvider.GetRequiredService<CategoryRepository>();
                var cache = serviceProvider.GetRequiredService<IDatabase>();
                return new CachedCategoryRepository(originalRepository, cache);
            });
            
            services.AddScoped<ProductRepository>();
            services.AddScoped<IProductRepository>(serviceProvider =>
            {
                var originalRepository = serviceProvider.GetRequiredService<ProductRepository>();
                var cache = serviceProvider.GetRequiredService<IDatabase>();
                return new CachedProductRepository(originalRepository, cache);
            });

            services.AddHttpContextAccessor();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ICepService, CepService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            // services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IImageRepository, ImageRepository>();
            // services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            return services;
        }
    }
}
