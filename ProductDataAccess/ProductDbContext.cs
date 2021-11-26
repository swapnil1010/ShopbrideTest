using Microsoft.EntityFrameworkCore;
using ProductModel;
using System;


namespace ProductDataAccess
{
    public class ProductDbContext: DbContext
    {
        public DbSet<Product> Products { get; set; }
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().ToTable("Product");

        }
    }
}
