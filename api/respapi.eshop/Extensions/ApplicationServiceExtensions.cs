using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using respapi.eshop.Data;
using respapi.eshop.Interfaces;
using respapi.eshop.Repositories;
using respapi.eshop.Services;
using respapi.eshop.Services.Cache;
using respapi.eshop.Services.Queue;
using respapi.eshop.Services.Queue.Workers;
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
    
            //connect to redis port
            services.AddSingleton(x => ConnectionMultiplexer.Connect("localhost:5555"));
            services.AddScoped<IDatabase>(x => x.GetRequiredService<ConnectionMultiplexer>().GetDatabase());

            services.AddScoped<CategoryRepository>();

            services.AddScoped<ICategoryRepository>(serviceProvider =>
            {
                var dbContext = serviceProvider.GetRequiredService<AppDbContext>();
                var mapper = serviceProvider.GetRequiredService<IMapper>();
                var categoryRepository = new CategoryRepository(dbContext, mapper);

                var cache = serviceProvider.GetRequiredService<IDatabase>();
                return new CachedCategoryRepository(categoryRepository, cache);
            });
            
            services.AddScoped<ProductRepository>();

            services.AddScoped<IProductRepository>(serviceProvider =>
            {
                var dbContext = serviceProvider.GetRequiredService<AppDbContext>();
                var productRepository = new ProductRepository(dbContext);

                var cache = serviceProvider.GetRequiredService<IDatabase>();
                return new CachedProductRepository(productRepository, cache);
            });

            //rabbitmq dependency injection
            services.AddSingleton<IMessageQueueService, RabbitMQService>();

            services.AddHostedService<OrderWorker>();
            services.AddScoped<IQueuedOrderRepository, QueuedOrderRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IUserDetailCacheService, UserDetailCacheService>();

            services.AddHttpContextAccessor();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ICepService, CepService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IImageRepository, ImageRepository>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            return services;
        }
    }
}
