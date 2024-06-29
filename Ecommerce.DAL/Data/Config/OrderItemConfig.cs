using Ecommerce.DAL.Entites.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.DAL.Data.Config
{
    public class OrderItemConfig : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            //OwnsOne: Configures a one-to-one relationship for the ProductItemOrder property.
            //means that ProductItemOrder is a complex type (owned entity) that does not have a separate table
            //but shares the table with the OrderItem entity.
            builder.OwnsOne(i => i.ProductItemOrder, io => 
            {
                //Indicates that the ProductItemOrder entity is owned by the OrderItem entity.
                io.WithOwner(); 
            }); 
            builder.Property(i => i.Price).HasColumnType("decimal(18,2)");
        }
    }
}
