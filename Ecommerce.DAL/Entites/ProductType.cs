namespace Ecommerce.DAL.Entites
{
    public class ProductType : BaseEntity<int>
    {
        public string Name { get; set; }
        public virtual ICollection<Product> Products { get; set; } = new HashSet<Product>();
    }
}