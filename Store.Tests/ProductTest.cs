using Newtonsoft.Json.Linq;
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
            decimal price = 2.00m;
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
            var product = new Product("Name", "Description", "Image", 10.0m, 2.0m);
            decimal initialQuantity = product.QuantityOnHand;
            decimal quantityToDecrease = 2.0m;

            // Act
            product.DecreaseStockQuantity(quantityToDecrease);

            // Assert
            Assert.That(product.QuantityOnHand, Is.EqualTo(initialQuantity - quantityToDecrease));
        }

        [Test]
        public void IncreaseStockQuantity_ShouldIncreaseStockQuantity_WhenCalled()
        {
            // Arrange
            var product = new Product("Name", "Description", "Image", 10.0m, 2.0m);
            decimal initialQuantity = product.QuantityOnHand;
            decimal quantityToIncrease = 1.0m;

            // Act
            product.IncreaseStockQuantity(quantityToIncrease);

            // Assert
            Assert.That(product.QuantityOnHand, Is.EqualTo(initialQuantity + quantityToIncrease));
        }

        [Test]
        public void UpdateDetails_ShouldUpdateDetails_WhenValidArgumentsAreGiven()
        {
            // Arrange
            var product = new Product("Name", "Description", "Image", 10.0m, 5.0m);
            string newName = "New Name";
            string newDescription = "New Description";
            string newImage = "newImage.jpg";
            decimal newValue = 15.0m;

            // Act
            product.UpdateDetails(newName, newDescription, newImage, newValue);

            // Assert
            Assert.That(product.Name, Is.EqualTo(newName));
            Assert.That(product.Description, Is.EqualTo(newDescription));
            Assert.That(product.Image, Is.EqualTo(newImage));
            Assert.That(product.Value, Is.EqualTo(newValue));
        }

        [Test]
        public void UpdateDetails_ShouldThrowArgumentException_WhenNameIsNullOrWhiteSpace()
        {
            // Arrange
            var product = new Product("Title", "Description", "Image", 10.0m, 5.0m);
            string? invalidName = null;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => product.UpdateDetails(invalidName, "Description", "Image", 15.0m));
            Assert.Throws<ArgumentException>(() => product.UpdateDetails("", "Description", "Image", 15.0m));
            Assert.Throws<ArgumentException>(() => product.UpdateDetails("   ", "Description", "Image", 15.0m));
        }

        [Test]
        public void UpdateDetails_ShouldThrowArgumentException_WhenValueIsZeroOrLess()
        {
            // Arrange
            var product = new Product("Name", "Description", "Image", 10.0m, 5.0m);
            decimal invalidValue = 0m;
            decimal negativeValue = -5.0m;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => product.UpdateDetails("Name", "Description", "Image", invalidValue));
            Assert.Throws<ArgumentException>(() => product.UpdateDetails("Name", "Description", "Image", negativeValue));
        }
    }
}