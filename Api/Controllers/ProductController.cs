using Api.Model;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Domain.Entities;
using Store.Domain.Repositories;
using Store.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        [HttpPost]
        [Route("v1/products")]
        public async Task<IActionResult> Add(ProductViewModel product)
        {
            var productMap = _mapper.Map<Product>(product);
            await _repository.CreateAsync(productMap);
            return Ok(Result<Product>.SuccessResult(productMap));
        }

        [HttpGet]
        [Route("v1/products")]
        [ResponseCache(Duration = 15)]
        public async Task<IActionResult> Get()
        {
            var products = await _repository.FindAll();
            var productViewModels = _mapper.Map<IEnumerable<ProductViewModel>>(products);
            return Ok(Result<IEnumerable<ProductViewModel>>.SuccessResult(productViewModels));
        }

        [HttpGet]
        [Route("v1/products/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var product = await _repository.FindByName(name);
            if (product == null)
                return NotFound(Result<ProductViewModel>.FailureResult("Product not found."));

            var productViewModel = _mapper.Map<ProductViewModel>(product);
            return Ok(Result<ProductViewModel>.SuccessResult(productViewModel));
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
