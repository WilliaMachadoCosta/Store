using Microsoft.EntityFrameworkCore;
using Store.Domain.Entities;
using Store.Infra.Mapping;

namespace Store.Infra.DataContext
{
    internal class ProductContext: DbContext
    {
        public ProductContext(DbContextOptions options) : base(options) =>
            Database.EnsureCreated();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductMap());    
        }

        public DbSet<Product> Products { get; set; }
    }
}
