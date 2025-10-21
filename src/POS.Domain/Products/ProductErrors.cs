using POS.SharedKernel;

namespace POS.Domain.Products;
public static class ProductErrors
{
    public static readonly Error VariantWithBarcodeAlreadyExists = Error.Conflict(
        "Product.VariantWithBarcodeAlreadyExists",
        "A variant with this Barcode already exists for the product.");

    public static readonly Error SkuIsEmpty = Error.Problem(
        "Product.SkuIsEmpty",
        "SKU cannot be empty.");

    public static readonly Error InvalidItemsPerCase = Error.Problem(
        "UnitOfMeasure.InvalidItemsPerCase",
        "Items per case must be greater than 1 for a case unit of measure.");

    public static readonly Error ItemsPerCaseForPieceNotAllowed = Error.Problem(
        "UnitOfMeasure.ItemsPerCaseForPieceNotAllowed",
        "Items per case should not be specified for a piece unit of measure.");
}
