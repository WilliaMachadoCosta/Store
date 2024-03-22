using Store.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Entities
{
    public class Product: Entity
    {
        public Product(
           string name, string description, string image, decimal value, decimal quantity)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("The product name must be provided.", nameof(name));

            if (value <= 0)
                throw new ArgumentException("The product value must be greater than zero.", nameof(value));

            if (quantity < 0)
                throw new ArgumentException("The product quantity cannot be negative", nameof(quantity));


            Name = name;
            Description = description;
            Image = image;
            Value = value;
            QuantityOnHand = quantity;
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Image { get; private set; }
        public decimal Value { get; private set; }
        public decimal QuantityOnHand { get; private set; }

        public override string ToString()
        {
            return Name;
        }

        public void DecreaseStockQuantity(decimal quantity)
        {
            if (quantity < 0)
                throw new ArgumentException("The quantity to be removed cannot be negative.", nameof(quantity));

            if (QuantityOnHand - quantity < 0)
                throw new InvalidOperationException("There is not enough stock to remove the specified quantity.");

            QuantityOnHand -= quantity;
            
        }

        public void IncreaseStockQuantity(decimal quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("The quantity to be added cannot be negative.", nameof(quantity));

            QuantityOnHand += quantity;
            
        }

        public void UpdateDetails(string name, string description, string image, decimal value)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("The product name must be provided.", nameof(name));

            if (value <= 0)
                throw new ArgumentException("The product value must be greater than zero.", nameof(value));

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
