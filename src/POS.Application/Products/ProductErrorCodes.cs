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
        public const string BrandDoesNotExist = nameof(BrandDoesNotExist);
    }

    public static class CreateVariant
    {
        public const string MissingBarcode = nameof(MissingBarcode);
        public const string BarcodeTooLong = nameof(BarcodeTooLong);
        public const string DuplicateBarcode = nameof(DuplicateBarcode);
        public const string NegativePrice = nameof(NegativePrice);
        public const string PricePrecisionExceeded = nameof(PricePrecisionExceeded);
        public const string MissingCurrencyCode = nameof(MissingCurrencyCode);
        public const string InvalidPackageType = nameof(InvalidPackageType);
        public const string NegativeQuantity = nameof(NegativeQuantity);
        public const string MismatchPackaging = nameof(MismatchPackaging);
        public const string InvalidMeasurementAmount = nameof(InvalidMeasurementAmount);
        public const string MeasurementAmountPrecisionExceeded = nameof(MeasurementAmountPrecisionExceeded);
        public const string MissingUnitOfMeasureCode = nameof(MissingUnitOfMeasureCode);
        public const string InvalidCurrencyCode = nameof(InvalidCurrencyCode);
        public const string InvalidUnitOfMeasureCode = nameof(InvalidUnitOfMeasureCode);
    }
}
