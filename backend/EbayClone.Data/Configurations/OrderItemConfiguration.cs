using EbayClone.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EbayClone.Data.Configurations
{
	public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
	{
		public void Configure(EntityTypeBuilder<OrderItem> builder)
		{
			builder
				 .HasKey(OrderItem => OrderItem.Id);
			builder
				 .Property(OrderItem => OrderItem.Id)
				 .UseIdentityColumn();

			builder
				 .HasOne(OrderItem => OrderItem.Item);
			builder
				 .Property(OrderItem => OrderItem.ItemId)
				 .IsRequired();

			builder
				.Property(i => i.Price)
				.IsRequired();

			builder
				 .HasOne(OrderItem => OrderItem.Order)
				 .WithMany(Order => Order.Items)
				 .HasForeignKey(OrderItem => OrderItem.OrderId)
				 // To avoid multiple cascade path error
				 .OnDelete(DeleteBehavior.NoAction);

			builder
				 .Property(OrderItem => OrderItem.Quantity)
				 .IsRequired();

			builder
				.ToTable("OrderItems");
		}
	}
}