using Store.Shared;

namespace Store.Domain.Entities
{
    public class Product: Entity
    {
        public Product(
           string name, string description, string image, decimal value, decimal quantityOnHand)
        {
            ProductValidator.ValidateName(name);
            ProductValidator.ValidateValue(value);
            ProductValidator.ValidateNonNegativeQuantity(quantityOnHand);

            Name = name;
            Description = description;
            Image = image;
            Value = value;
            QuantityOnHand = quantityOnHand;
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Image { get; private set; }
        public decimal Value { get; private set; }
        public decimal QuantityOnHand { get; private set; }

        public void DecreaseStockQuantity(decimal quantity)
        {
            ProductValidator.ValidateNonNegativeQuantity(quantity);
            ProductValidator.ValidateSufficientStockForRemoval(QuantityOnHand, quantity);

            QuantityOnHand -= quantity;           
        }

        public void IncreaseStockQuantity(decimal quantity)
        {
            ProductValidator.ValidateNonNegativeQuantity(quantity);

            QuantityOnHand += quantity;          
        }

        public void UpdateDetails(string name, string description, string image, decimal value)
        {
            ProductValidator.ValidateName(name);
            ProductValidator.ValidateValue(value);

            Name = name;
            Description = description;
            Image = image;
            Value = value;
        }

        public bool IsInStock()
        {
            return QuantityOnHand > 0;
        }

    }
}
