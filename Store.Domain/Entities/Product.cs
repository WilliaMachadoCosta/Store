using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Entities
{
    public class Product
    {
        public Product(
           string name, string description, string image, decimal value, decimal quantity)
        {
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
            QuantityOnHand -= quantity;
        }
    }
}
