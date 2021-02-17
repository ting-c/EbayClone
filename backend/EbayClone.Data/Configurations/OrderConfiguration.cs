using EbayClone.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EbayClone.Data.Configurations
{
	public class OrderConfiguration : IEntityTypeConfiguration<Order>
	{
		public void Configure(EntityTypeBuilder<Order> builder)
		{
			builder
				.HasKey(u => u.Id);

			builder
				.Property(u => u.Id)
				.UseIdentityColumn();

			builder
				.Property(u => u.Date)
				.IsRequired();

			builder
				.ToTable("Orders");
		}
	}
}