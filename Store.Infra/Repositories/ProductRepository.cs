using Microsoft.EntityFrameworkCore;
using Store.Domain.Entities;
using Store.Domain.Repositories;
using Store.Infra.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Infra.Repositories
{
    public class ProductRepository: IProductRepository
    {
        protected readonly ProductContext productContext;
        public ProductRepository(ProductContext context) =>
            productContext = context;

        public async Task<Product> CreateAsync(Product product)
        {
            productContext.Add(product);
            await productContext.SaveChangesAsync();
            return product;
        }

        public async Task<IEnumerable<Product>> FindAll()
        {
            var products = await productContext.Products.AsNoTracking().OrderBy(product => product.Id).ToListAsync();
            return products;
        }

        public async Task<Product?> FindByName(string name)
        {
            return await productContext.Products.FirstOrDefaultAsync(p => p.Name == name);
        }

        public async Task Remove(Product product)
        {
            productContext.Set<Product>().Remove(product);
            await productContext.SaveChangesAsync();
        }

        public async Task<Product> Update(Product product)
        {
            productContext.Set<Product>().Update(product);
            await productContext.SaveChangesAsync();
            return product;
        }
    }
}
