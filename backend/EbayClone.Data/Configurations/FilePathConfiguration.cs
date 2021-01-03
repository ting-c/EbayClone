using EbayClone.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EbayClone.Data.Configurations
{
    public class FilePathConfiguration : IEntityTypeConfiguration<FilePath>
    {
        public void Configure(EntityTypeBuilder<FilePath> builder)
        {
            builder
                .HasKey(f => f.Id);
            builder
                .Property(f => f.Id)
                .UseIdentityColumn();

            builder
                .Property(f => f.UrlPath)
                .IsRequired();

            builder
                .Property(f => f.ItemId)
                .IsRequired();
            builder
                .HasOne(f => f.Item)
                .WithMany(i => i.ImageUrl)
                .HasForeignKey(f => f.ItemId);
   
            builder
                .Property(f => f.UserId)
                .IsRequired();
            builder
                .HasOne(f => f.User)
                .WithMany(u => u.ImageUrl)
                .HasForeignKey(f => f.UserId)
                // To avoid multiple cascade path error
                .OnDelete(DeleteBehavior.NoAction); 

            builder
                .ToTable("FilePaths");
        }
    }
}