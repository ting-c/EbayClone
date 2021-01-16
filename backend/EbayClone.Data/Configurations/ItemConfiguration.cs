using EbayClone.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EbayClone.Data.Configurations
{
    public class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder
                .HasKey(i => i.Id);

            builder
                .Property(i => i.Id)
                .UseIdentityColumn();

            builder
                .Property(i => i.Title)
                .IsRequired()
                .HasMaxLength(100);
            builder
                .Property(i => i.Description)
                .HasMaxLength(800);
            builder
                .Property(i => i.Price)
                .IsRequired();
            builder
                .Property(i => i.Quantity)
                .IsRequired();
            builder
                .Property(i => i.Condition)
                .IsRequired()
                .HasMaxLength(50);
            builder
                .Property(i => i.IsAuction)
                .IsRequired();
            builder
                .Property(i => i.SellerId)
                .IsRequired();
            builder
                .HasOne(i => i.Seller)
                .WithMany(u => u.SellingItems)
                .HasForeignKey(i => i.SellerId);
                
            builder
                .ToTable("Items");
        }
    }
}