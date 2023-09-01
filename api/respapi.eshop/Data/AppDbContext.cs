using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using respapi.eshop.Models.Entities;
using static respapi.eshop.Data.AppDbContext;

namespace respapi.eshop.Data
{
    public class AppDbContext : IdentityDbContext<AppUser, IdentityRole<int>, int>       
    {
        public DbSet<UserAdress> Addresses { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<UserAdress> UserAdresses { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<UserAdress>()
            //    .HasOne(p => p.AppUser)
            //    .WithMany(c => c.Adresses)
            //    .HasForeignKey(c => c.AppUserId)
            //    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AppUser>()
               .HasMany(p => p.Adresses)
               .WithOne(c => c.AppUser)
               .HasForeignKey(c => c.AppUserId)
               .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
