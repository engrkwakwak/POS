namespace POS.Application.Products;
public static class ProductErrorCodes
{
    public static class CreateProduct
    {
        public const string MissingName = nameof(MissingName);
        public const string NameTooLong = nameof(NameTooLong);

        public const string NullDescription = nameof(NullDescription);
        public const string DescriptionTooLong = nameof(DescriptionTooLong);

        public const string InvalidCategory = nameof(InvalidCategory);

        public const string MissingBrandId = nameof(MissingBrandId);
    }
}
