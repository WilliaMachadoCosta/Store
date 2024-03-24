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
    }
}
