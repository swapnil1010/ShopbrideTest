using Microsoft.EntityFrameworkCore;
using ProductDataAccess;
using ProductModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductAPI.Repositories.ProductRepository
{
    public class ProductRepository: IProductRepository
    {
        private readonly ProductDbContext productDbContext;

        public ProductRepository(ProductDbContext productDbContext)
        {
            this.productDbContext = productDbContext;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await productDbContext.Products.ToListAsync();
        }

        public async Task<Product> GetProduct(int productId)
        {
            return await productDbContext.Products
                .FirstOrDefaultAsync(p => p.ProductId == productId);
        }

        public async Task<Product> AddProduct(Product product)
        {
            var result = await productDbContext.Products.AddAsync(product);
            await productDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            var result = await productDbContext.Products
                .FirstOrDefaultAsync(e => e.ProductId == product.ProductId);

            if (result != null)
            {
                result.ProductName = product.ProductName;
                result.ProductPrice = product.ProductPrice;
                result.ProductDescription = product.ProductDescription;

                await productDbContext.SaveChangesAsync();

                return result;
            }

            return null;
        }

        public async Task<Product> DeleteProduct(int productId)
        {
            var result = await productDbContext.Products
                .FirstOrDefaultAsync(e => e.ProductId == productId);
            if (result != null)
            {
                productDbContext.Products.Remove(result);
                await productDbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<IEnumerable<Product>> Search(string name)
        {
            IQueryable<Product> query = productDbContext.Products;

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(e => e.ProductName.Contains(name));
            }
            return await query.ToListAsync();
        }
    }
}
