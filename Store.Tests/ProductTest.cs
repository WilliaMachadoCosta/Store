using Store.Domain.Entities;

namespace Store.Tests
{
    [TestFixture]
    public class ProductTest
    {
        [Test]
        public void Constructor_ShouldInstantiateProduct_WhenValidParametersAreGiven()
        {
            // Arrange
            string name = "Product Name";
            string description = "Product Description";
            string image = "product.jpg";
            decimal price = 5.99m;
            decimal quantity = 100;

            // Act
            var product = new Product(name, description, image, price, quantity);

            // Assert
            Assert.IsNotNull(product);
            Assert.That(product.Name, Is.EqualTo(name));
            Assert.That(product.Description, Is.EqualTo(description));
            Assert.That(product.Image, Is.EqualTo(image));
            Assert.That(product.Value, Is.EqualTo(price));
            Assert.That(product.QuantityOnHand, Is.EqualTo(quantity));
        }

        [Test]
        public void DecreaseQuantity_ShouldDecreaseQuantity_WhenCalled()
        {
            // Arrange
            var product = new Product("Name", "Description", "Image", 10.0m, 5.0m);
            decimal initialQuantity = product.QuantityOnHand;
            decimal quantityToDecrease = 2.0m;

            // Act
            product.DecreaseStockQuantity(quantityToDecrease);

            // Assert
            Assert.That(product.QuantityOnHand, Is.EqualTo(3.0m));
        }
    }
}