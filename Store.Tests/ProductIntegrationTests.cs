using Microsoft.EntityFrameworkCore;
using Moq;
using Store.Domain.Entities;
using Store.Domain.Repositories;
using Store.Infra.DataContext;
using Store.Infra.Repositories;
using Store.Shared;

namespace Store.Tests
{
    [TestFixture]
    public class ProductIntegrationTests
    {

        private ProductContext _dbContext;
        private IProductRepository _productRepository;

        [SetUp]
        public void Setup()
        {    
            var options = new DbContextOptionsBuilder<ProductContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDatabase")
                .Options;

            _dbContext = new ProductContext(options);
            _productRepository = new ProductRepository(_dbContext);
        }

        [TearDown]
        public void TearDown()
        {
            _dbContext.Dispose();
        }

        [Test]
        public async Task CreateProduct_ShouldSucceed()
        {
            // Arrange
            var product = new Product("Test Product", "Test Description", "test.jpg", 10.99m, 100);

            // Act
            var createdProduct = await _productRepository.CreateAsync(product);

            // Assert
            Assert.IsNotNull(createdProduct);
            Assert.That(createdProduct.Name, Is.EqualTo(product.Name));
        }

        [Test]
        public async Task UpdateProduct_ShouldSucceed()
        {
            // Arrange
            var originalProductName = "Test Product";
            var updatedProductName = "Product New";
            var product = new Product(originalProductName, "Test Description", "test.jpg", 10.99m, 100);
            var createdProduct = await _productRepository.CreateAsync(product);

            
            createdProduct.UpdateDetails(updatedProductName, "Updated Description", "updated.jpg", 15.99m);

            // Act
            var productUpdated = await _productRepository.Update(createdProduct);

            // Assert
            Assert.IsNotNull(productUpdated);
            Assert.That(productUpdated.Name, Is.EqualTo(updatedProductName));
  
        }

        [Test]
        public async Task FindAllProducts_ShouldReturnAllProducts()
        {
            // Arrange
            var product1 = new Product("Product 1", "Description 1", "image1.jpg", 10.99m, 50);
            var product2 = new Product("Product 2", "Description 2", "image2.jpg", 20.99m, 30);
            await _productRepository.CreateAsync(product1);
            await _productRepository.CreateAsync(product2);

            // Act
            var products = await _productRepository.FindAll();

            // Assert
            Assert.IsNotNull(products);
            Assert.IsTrue(products.Any(p => p.Name == "Product 1")); 
            Assert.IsTrue(products.Any(p => p.Name == "Product 2")); 
                                                                     
        }

        [Test]
        public async Task FindProductByName_ShouldReturnProduct()
        {
            // Arrange
            var productName = "Test Product";
            var product = new Product(productName, "Test Description", "test.jpg", 10.99m, 100);
            await _productRepository.CreateAsync(product);

            // Act
            var foundProduct = await _productRepository.FindByName(productName);

            // Assert
            Assert.IsNotNull(foundProduct);
            Assert.AreEqual(productName, foundProduct.Name);
        }

        [Test]
        public async Task FindProductById_ShouldReturnProduct()
        {
            // Arrange
            var idDiff = new Guid();

            var product = new Product("Test Product", "Test Description", "test.jpg", 10.99m, 100);
        
            var  createdProduct = await _productRepository.CreateAsync(product);

            // Act
            var foundProduct = await _productRepository.FindById(createdProduct.Id);

            // Assert
            Assert.IsNotNull(foundProduct);
            Assert.That(foundProduct.Id, Is.EqualTo(createdProduct.Id));
        }

        [Test]
        public async Task FindAll_ReturnsListOfProducts()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product("Product 1", "Description 1", "image1.jpg", 10.0m, 100),
                new Product("Product 2", "Description 2", "image2.jpg", 20.0m, 200)
            };

            var mockRepository = new Mock<IProductRepository>();
            mockRepository.Setup(repo => repo.FindAll(
            It.IsAny<Func<IQueryable<Product>, IOrderedQueryable<Product>>>(), // esperando a função orderByFunc
                null, // skip
                null // take
                )).ReturnsAsync(products);

            var repository = mockRepository.Object;

            // Act
            var result = await repository.FindAll(null, null, null); // passando null para os novos parâmetros


            // Assert
            Assert.That(result.Count(), Is.EqualTo(products.Count));
            Assert.That(result.First().Name, Is.EqualTo(products.First().Name));
            Assert.That(result.Last().Name, Is.EqualTo(products.Last().Name));
        }

        [Test]
        public async Task FindByName_ReturnsCorrectProduct()
        {
            // Arrange
            var productName = "Product 1";
            var product = new Product(productName, "Description 1", "image1.jpg", 10.0m, 100);

            var mockRepository = new Mock<IProductRepository>();
            mockRepository.Setup(repo => repo.FindByName(productName)).ReturnsAsync(product);

            var repository = mockRepository.Object;

            // Act
            var result = await repository.FindByName(productName);
            
            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Name, Is.EqualTo(productName));
        }

        [Test]
        public async Task Create_ProductCreatedSuccessfully()
        {
            // Arrange
            var product = new Product("Product 1", "Description 1", "image1.jpg", 10.0m, 100);

            var mockRepository = new Mock<IProductRepository>();
            mockRepository.Setup(repo => repo.CreateAsync(product)).ReturnsAsync(product);

            var repository = mockRepository.Object;

            // Act
            var result = await repository.CreateAsync(product);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Name, Is.EqualTo(product.Name));
        }

        [Test]
        public async Task Update_ProductUpdatedSuccessfully()
        {
            // Arrange
            var product = new Product("Product 1", "Description 1", "image1.jpg", 10.0m, 100);

            var mockRepository = new Mock<IProductRepository>();
            mockRepository.Setup(repo => repo.Update(product)).ReturnsAsync(product);

            var repository = mockRepository.Object;

            // Act
            var result = await repository.Update(product);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Name, Is.EqualTo(product.Name));
        }

        [Test]
        public async Task Remove_ProductRemovedSuccessfully()
        {
            // Arrange
            var product = new Product("Product 1", "Description 1", "image1.jpg", 10.0m, 100);

            var mockRepository = new Mock<IProductRepository>();
            mockRepository.Setup(repo => repo.Remove(product)).Returns(Task.CompletedTask);

            var repository = mockRepository.Object;

            // Act & Assert
            Assert.DoesNotThrowAsync(async () => await repository.Remove(product));
        }

    }
}
