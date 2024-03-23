using Api.Model;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Domain.Entities;
using Store.Domain.Repositories;
using Store.Shared;

namespace Api.Controllers
{
    public class ProductController : Controller
    {
        private readonly IMapper mapper;
        private readonly IProductRepository repository;

        public ProductController(IMapper mapper, IProductRepository repository)
        {
            this.mapper = mapper;
            this.repository = repository;
        }


        [HttpPost]
        [Route("v1/products")]
        public async Task<IActionResult> Add(ProductViewModel product)
        {
            var productMap = mapper.Map<Product>(product);
            await repository.CreateAsync(productMap);
            return Ok(productMap);
        }

        [HttpGet]
        [Route("v1/products")]
        [ResponseCache(Duration = 15)]
        public async Task<IEnumerable<ProductViewModel>> Get()
        {
            var products = await repository.FindAll();
            return mapper.Map<IEnumerable<ProductViewModel>>(products);   
        }


        [HttpGet]
        [Route("v1/products/{name}")]
        public async Task<ProductViewModel> GetByName(string name)
        {
            var product = await repository.FindByName(name);
            return mapper.Map<ProductViewModel>(product);
        }

        //[HttpPut]
        //[Route("v1/products/{id}")]
        //public async Task<Product> Update(int id, ProductViewModel product)
        //{
        //    var existingProduct = await repository.get
        //    if (existingProduct == null)
        //    {
        //        // Produto não encontrado, você pode retornar um código de status HTTP adequado
        //        // e uma mensagem de erro
        //        return NotFound(); // Ou BadRequest(), por exemplo
        //    }

        //    // Atualize apenas as propriedades relevantes do produto existente com base nos dados recebidos
        //    existingProduct.Name = product.Name;
        //    existingProduct.Description = product.Description;
        //    existingProduct.Price = product.Price;
        //    // Adicione outras propriedades que você deseja atualizar

        //    // Agora, atualize o produto no repositório
        //    await repository.Update(existingProduct);

        //    return existingProduct;
        //}


        [HttpDelete]
        [Route("v1/products/")]
        public async Task<IActionResult> Remove(ProductViewModel product)
        {
            var productMap = mapper.Map<Product>(product);
            await repository.Remove(productMap);
            return Ok("Product deleted successfully.");
        }
    }
}
