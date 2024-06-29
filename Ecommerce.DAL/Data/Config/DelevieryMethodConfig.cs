using Ecommerce.DAL.Entites.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.DAL.Data.Config
{
    public class DelevieryMethodConfig : IEntityTypeConfiguration<DeleiveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeleiveryMethod> builder)
        {
            builder.Property(d => d.Price)
                .HasColumnType("decimal(18,2)");
        }
    }
}
