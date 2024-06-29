using Ecommerce.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Ecommerce.DAL.Data.Config
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Description).IsRequired().HasMaxLength(500);
            builder.Property(p => p.Price).HasColumnType("decimal(18,2)");

            builder.HasOne(p => p.ProductType)
                  .WithMany(pt => pt.Products)
                  .HasForeignKey(p => p.ProductTypeId)
                  .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.ProductBrand)
                   .WithMany(pb => pb.Products)
                   .HasForeignKey(p => p.ProductBrandId)
                   .OnDelete(DeleteBehavior.Restrict);

            //builder.HasOne(p => p.Category)
            //       .WithMany(c => c.Products)
            //       .HasForeignKey(p => p.CategoryId)
            //       .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
