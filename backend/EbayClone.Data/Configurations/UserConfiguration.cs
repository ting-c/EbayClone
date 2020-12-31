using EbayClone.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EbayClone.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .HasKey(u => u.Id);

            builder
                .Property(u => u.Id)
                .UseIdentityColumn();
            
            builder
                .HasIndex(u => u.UserName)
                .IsUnique();

            builder
                .Property(u => u.UserName)
                .IsRequired()
                .HasMaxLength(20);

            builder
                .HasIndex(u => u.Email)
                .IsUnique();
            
            builder
                .Property(u => u.Email)
                .IsRequired();

            builder
                .Property(u => u.FirstName)
                .IsRequired()
                .HasMaxLength(20);

            builder
                .Property(u => u.LastName)
                .IsRequired()
                .HasMaxLength(20);

            builder
                .Property(u => u.Address)
                .IsRequired()
                .HasMaxLength(300);

            builder
                .ToTable("Users");
        }
    }
}