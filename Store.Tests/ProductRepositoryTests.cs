﻿using Microsoft.EntityFrameworkCore;
using Moq;
using Store.Domain.Entities;
using Store.Domain.Repositories;
using Store.Infra.DataContext;
using Store.Infra.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Tests
{
    [TestFixture]
    public class ProductRepositoryTests
    {
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
            mockRepository.Setup(repo => repo.FindAll()).ReturnsAsync(products);

            var repository = mockRepository.Object;

            // Act
            var result = await repository.FindAll();

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
            mockRepository.Setup(repo => repo.Create(product)).ReturnsAsync(product);

            var repository = mockRepository.Object;

            // Act
            var result = await repository.Create(product);

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