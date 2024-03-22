using Store.Domain.Entities;
using Store.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> FindAll();
        Task<Product> FindByName(string name);
        Task<Product> Create(Product product);
        Task<Product> Update(Product product);
        Task Remove(Product product);
    }
}
