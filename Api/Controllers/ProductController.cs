using Api.Model;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Store.Domain.Entities;
using Store.Domain.Repositories;
using Store.Shared;

namespace Api.Controllers
{
    public class ProductController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _repository;

        public ProductController(IMapper mapper, IProductRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        [HttpGet]
        [Route("v1/products/byname/{name}")]
        [ResponseCache(Duration = 15)]
        public async Task<IActionResult> GetByName(string name)
        {
            var product = await _repository.FindByName(name);
            if (product == null)
                return NotFound(Result<ProductViewModel>.FailureResult("Product not found."));

            var productViewModel = _mapper.Map<ProductViewModel>(product);
            return Ok(Result<ProductViewModel>.SuccessResult(productViewModel));
        }

        [HttpGet]
        [Route("v1/products/{id}")]
        [ResponseCache(Duration = 15)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var product = await _repository.FindById(id);
            if (product == null)
                return NotFound(Result<ProductViewModel>.FailureResult("Product not found."));

            var productViewModel = _mapper.Map<ProductViewModel>(product);
            return Ok(Result<ProductViewModel>.SuccessResult(productViewModel));
        }

        [HttpGet]
        [Route("v1/products")]
        [ResponseCache(Duration = 15)]
        public async Task<IActionResult> GetProducts(
           string orderBy = "Id", int? skip = null, int? take = null, bool isDescending = false)
        {
            Func<IQueryable<Product>, IOrderedQueryable<Product>> orderByFunc = null;

            switch (orderBy.ToLower())
            {
                case "id":
                    orderByFunc = q => isDescending ? q.OrderByDescending(p => p.Id) : q.OrderBy(p => p.Id);
                    break;
                case "name":
                    orderByFunc = q => isDescending ? q.OrderByDescending(p => p.Name) : q.OrderBy(p => p.Name);
                    break;
                case "date":
                    orderByFunc = q => isDescending ? q.OrderByDescending(p => p.CreatedDate) : q.OrderBy(p => p.CreatedDate);
                    break;
                default:
                    orderByFunc = q => isDescending ? q.OrderByDescending(p => p.Id) : q.OrderBy(p => p.Id);
                    break;
            }

            var products = await _repository.FindAll(orderByFunc, skip, take);

            return Ok(Result<IEnumerable<Product>>.SuccessResult(products));
        }

        [HttpPost]
        [Route("v1/products")]
        public async Task<IActionResult> Add(ProductViewModel product)
        {
            var productMap = _mapper.Map<Product>(product);
            await _repository.CreateAsync(productMap);
            return Ok(Result<Product>.SuccessResult(productMap));
        }

        [HttpPut]
        [Route("v1/products/{id}")]
        public async Task<IActionResult> Update(Guid id, ProductViewModel product)
        {
            var existingProduct = await _repository.FindById(id);
            if (existingProduct == null)
            {
                return NotFound(Result<Product>.FailureResult("Product not found."));
            }

            existingProduct.UpdateDetails(product.Name, product.Description, product.Image, product.Value);

            await _repository.Update(existingProduct);

            return Ok(Result<Product>.SuccessResult(existingProduct));
        }

        [HttpDelete]
        [Route("v1/products/")]
        public async Task<IActionResult> Remove(Guid id)
        {
            var existingProduct = await _repository.FindById(id);
            if (existingProduct == null)
            {
                return NotFound(Result<Product>.FailureResult("Product not found."));
            }

            await _repository.Remove(existingProduct);

            return Ok(Result<string>.SuccessResult("Product deleted successfully."));
        }
    }
}
