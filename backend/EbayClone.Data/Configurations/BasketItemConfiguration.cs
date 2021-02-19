using EbayClone.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EbayClone.Data.Configurations
{
	public class BasketItemConfiguration : IEntityTypeConfiguration<BasketItem>
	{
		public void Configure(EntityTypeBuilder<BasketItem> builder)
		{
			builder
				 .HasKey(b => b.Id);
			builder
				 .Property(b => b.Id)
				 .UseIdentityColumn();

            builder
                .HasOne(b => b.Item);
            builder
                .Property(b => b.ItemId)
                .IsRequired();

            builder
                .HasOne(b => b.User)
                .WithMany(u => u.Basket)
                .HasForeignKey(b => b.UserId)
                 // To avoid multiple cascade path error
                .OnDelete(DeleteBehavior.NoAction);

			builder
                .Property(b => b.UserId)
                .IsRequired();

            builder
                .Property(b => b.Quantity)
                .IsRequired();

			builder
				.ToTable("BasketItems");
		}
	}
}