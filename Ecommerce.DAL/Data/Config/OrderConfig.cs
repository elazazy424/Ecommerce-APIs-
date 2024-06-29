using Ecommerce.DAL.Entites.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.DAL.Data.Config
{
    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            //one to one relationship 
            builder.OwnsOne(o => o.ShipToAddress, a =>
            {
                //Indicates that the ShipToAddress entity is owned by the Order entity.
                a.WithOwner();
            });
            //Specifies that the ShipToAddress navigation property is required,
            //meaning it must have a value.
            builder.Navigation(a => a.ShipToAddress).IsRequired();

            //Specifies that the OrderStatus property should be stored as a string in the database.
            builder.Property(a => a.Status)
                .HasConversion(
                 o => o.ToString(),
                 o => (OrderStatus)Enum.Parse(typeof(OrderStatus), o, true)
                 );

            //HasMany: Configures a one-to-many relationship between Order and OrderItems
            //WithOne: Specifies that the OrderItem entity has a single owner (the Order).
            //OnDelete(DeleteBehavior.Cascade): Specifies that when an Order is deleted, all related OrderItems should also be deleted (cascade delete).
            builder.HasMany(o => o.OrderItems).WithOne().OnDelete(DeleteBehavior.Cascade);


        }
    }
}
