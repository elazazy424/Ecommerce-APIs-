//using Ecommerce.DAL.Entites;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;

//namespace Ecommerce.DAL.Data.Config
//{
//    public class CategoryConfig : IEntityTypeConfiguration<Category>
//    {
//        public void Configure(EntityTypeBuilder<Category> builder)
//        {
//            builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
//            builder.Property(c => c.Description).IsRequired().HasMaxLength(500);
//        }
//    }
//}
