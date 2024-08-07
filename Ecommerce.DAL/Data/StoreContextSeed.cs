﻿using Ecommerce.DAL.Entites;
using Ecommerce.DAL.Entites.OrderAggregate;
using System.Text.Json;
namespace Ecommerce.DAL.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            if (!context.ProductBrands.Any())
            {
                var brandsData = File.ReadAllText("../Ecommerce.DAL/Data/SeedData/brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                foreach (var item in brands)
                {
                    context.ProductBrands.Add(item);
                }
                await context.SaveChangesAsync();
            }
            if (!context.ProductTypes.Any())
            {
                var typesData = File.ReadAllText("../Ecommerce.DAL/Data/SeedData/types.json");
                var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                foreach (var item in types)
                {
                    context.ProductTypes.Add(item);
                }
                await context.SaveChangesAsync();
            }
            if (!context.Products.Any())
            {
                var productsData = File.ReadAllText("../Ecommerce.DAL/Data/SeedData/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                foreach (var item in products)
                {
                    context.Products.Add(item);
                }
                await context.SaveChangesAsync();
            }
            if (!context.DeleiveryMethods.Any())
            {
                var dmData = File.ReadAllText("../Ecommerce.DAL/Data/SeedData/delivery.json");
                var deliveryMethods = JsonSerializer.Deserialize<List<DeleiveryMethod>>(dmData);
                foreach (var item in deliveryMethods)
                {
                    context.DeleiveryMethods.Add(item);
                }
                await context.SaveChangesAsync();
            }
            
            
        }
    }
}
