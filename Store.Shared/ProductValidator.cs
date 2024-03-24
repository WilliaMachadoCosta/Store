namespace Store.Shared
{
    public static class ProductValidator
    {
        public static void ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("The product name must be provided.", nameof(name));
        }

        public static void ValidateValue(decimal value)
        {
            if (value <= 0)
                throw new ArgumentException("The product value must be greater than zero.", nameof(value));
        }

        public static void ValidateNonNegativeQuantity(decimal quantity)
        {
            if (quantity < 0)
                throw new ArgumentException("The quantity to be removed/added cannot be negative.", nameof(quantity));
        }

        public static void ValidateSufficientStockForRemoval(decimal quantityOnHand, decimal quantity)
        {
            if (quantityOnHand - quantity < 0)
                throw new InvalidOperationException("There is not enough stock to remove the specified quantity.");
        }
    }
}
