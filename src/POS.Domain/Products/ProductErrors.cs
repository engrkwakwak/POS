using System.Net.NetworkInformation;
using POS.SharedKernel;

namespace POS.Domain.Products;
public static class ProductErrors
{
    public static readonly Error DuplicateVariant = Error.Conflict(
        "Product.DuplicateVariant",
        "A variant with this Barcode already exists for the product.");

    public static readonly Error NotFound = Error.NotFound(
        "Product.NotFound",
        "The product with the specified identifier was not found");

    public static class UnitOfMeasure
    {
        public static readonly Error InvalidItemsPerCase = Error.Problem(
            "UnitOfMeasure.InvalidItemsPerCase",
            "Items per case must be greater than 1 for a case unit of measure.");

        public static readonly Error ItemsPerCaseForPieceNotAllowed = Error.Problem(
            "UnitOfMeasure.ItemsPerCaseForPieceNotAllowed",
            "Items per case should not be specified for a piece unit of measure.");
    }

    public static class Measurement
    {
        public static readonly Error InvalidValue = Error.Problem(
            "Measurement.InvalidValue",
            "The measurement value must be greater than zero.");
    }
    
    public static class Packaging
    {
        public static readonly Error InvalidBoxQuantity = Error.Problem(
            "Packaging.InvalidBoxQuantity",
            "A box must contain at least 2 items. Use 'Piece' for single items.");

        public static readonly Error InvalidType = Error.Problem(
            "Packaging.InvalidType",
            "The package type is not supported.");
    }

    public static class Sku
    {
        public static readonly Error Empty = Error.Problem(
            "Sku.Empty",
            "Sku cannot be empty.");

        public static readonly Error InvalidFormat = Error.Problem(
            "Sku.InvalidFormat",
            "The sku format is invalid. It must be 'SKU-' followed by an 8-character uppercase hash (e.g., SKU-A9F3B1C8).");
    }

    public static class ProductCode
    {
        public static readonly Error Empty = Error.Problem(
            "ProductCode.Empty",
            "Product code cannot be empty.");

        public static readonly Error InvalidFormat = Error.Problem(
            "ProductCode.InvalidFormat",
            "The product code format is invalid. It must be 'PROD-' followed by an 8-character uppercase hash (e.g., PROD-A9F3B1C8).");
    }

}
