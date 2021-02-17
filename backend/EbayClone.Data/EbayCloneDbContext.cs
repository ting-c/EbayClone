using EbayClone.Core.Models;
using EbayClone.Data.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EbayClone.Data
{
    public class EbayCloneDbContext : IdentityDbContext<User, Role, int>
    {
        public DbSet<Item> Items { get; set; }
        public override DbSet<User> Users { get; set; }
        public DbSet<FilePath> FilePaths { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
        public DbSet<Order> Orders { get; set; }

        public EbayCloneDbContext(DbContextOptions<EbayCloneDbContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder builder)
        {
			// apply Identity’s configuration when generating migrations and configurations
			base.OnModelCreating(builder);

            builder
                .ApplyConfiguration(new ItemConfiguration());

            builder
                .ApplyConfiguration(new UserConfiguration());

            builder
                .ApplyConfiguration(new FilePathConfiguration());

            builder
                .ApplyConfiguration(new BasketItemConfiguration());

            builder
                .ApplyConfiguration(new OrderConfiguration());
        }
    }
}