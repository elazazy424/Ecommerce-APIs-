namespace Ecommerce.DAL.Entites
{
    public class Product:BaseEntity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        
        public int ProductTypeId { get; set; }
        public virtual ProductType ProductType { get; set; }
        public int ProductBrandId { get; set; }
        public virtual ProductBrand ProductBrand{ get; set; }
        ////categoryId is a foreign key to the Category table
        //public int CategoryId { get; set; }
        //public virtual Category Category { get; set; }
    }
    
    
}
