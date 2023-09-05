using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using respapi.eshop.Models.Entities;
using static respapi.eshop.Data.AppDbContext;

namespace respapi.eshop.Data
{
    public class AppDbContext : IdentityDbContext<AppUser, IdentityRole<int>, int>       
    {       
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<UserAdress> UserAdresses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AppUser>()
               .HasMany(p => p.Adresses)
               .WithOne(c => c.AppUser)
               .HasForeignKey(c => c.AppUserId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Category>()
                .HasMany(x => x.SubCategories)
                .WithOne(x => x.Category)
                .HasForeignKey(x=> x.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Product>()
                .HasOne(x => x.SubCategory)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.SubCategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
