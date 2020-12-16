using EbayClone.Core.Models;
using EbayClone.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace EbayClone.Data
{
    public class EbayCloneDbContext : DbContext
    {
        public DbSet<Item> Items { get; set; }
        public DbSet<User> Users { get; set; }

        public EbayCloneDbContext(DbContextOptions<EbayCloneDbContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .ApplyConfiguration(new ItemConfiguration());

            builder
                .ApplyConfiguration(new UserConfiguration());
        }
    }
}