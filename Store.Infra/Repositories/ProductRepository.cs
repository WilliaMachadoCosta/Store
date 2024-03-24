using Microsoft.EntityFrameworkCore;
using Store.Domain.Entities;
using Store.Domain.Repositories;
using Store.Infra.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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

        public async Task<IEnumerable<Product>> FindAll(
        Func<IQueryable<Product>, IOrderedQueryable<Product>> orderBy = null,
        int? skip = null,
        int? take = null)
        {
            IQueryable<Product> query = productContext.Products.AsNoTracking();

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return await query.ToListAsync();
        }


        public async Task<Product> FindById(Guid id)
        {
            return await productContext.Products.FirstOrDefaultAsync(p => p.Id == id);
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
