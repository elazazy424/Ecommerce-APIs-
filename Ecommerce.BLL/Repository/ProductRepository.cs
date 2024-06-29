using Ecommerce.BLL.Interfaces;
using Ecommerce.DAL.Data;
using Ecommerce.DAL.Entites;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.BLL.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context)
        {
                _context = context;
        }

        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
        {
            return await _context.ProductBrands.ToListAsync();
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            var product = await _context.Products
                .Include(p => p.ProductType)
                .Include(p => p.ProductBrand)
                .FirstOrDefaultAsync(p => p.Id == id);

            return product;
        }


        public async Task<IReadOnlyList<Product>> GetProductsAsync()
        {
            return await _context.Products.Include(p => p.ProductBrand).Include(p => p.ProductType).ToListAsync();
        }

        public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
        {
            return await _context.ProductTypes.ToListAsync();
        }
    }
}
