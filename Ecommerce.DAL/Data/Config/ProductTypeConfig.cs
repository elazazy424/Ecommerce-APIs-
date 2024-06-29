using Ecommerce.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.DAL.Data.Config
{
    public class ProductTypeConfig : IEntityTypeConfiguration<ProductType>
    {
        public void Configure(EntityTypeBuilder<ProductType> builder)
        {
            builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
            
        }
    }
}

