using POS.Domain.Products;

namespace POS.Application.Products.CreateVariant;

public sealed record CreateVariantRequest(
    Guid ProductId,
    string Barcode,
    decimal PriceAmount,
    string PriceCurrencyCode,
    PackageType PackageType,
    int PackageQuantity,
    decimal MeasurementAmount,
    string UnitOfMeasureCode,
    bool IsVatable);
