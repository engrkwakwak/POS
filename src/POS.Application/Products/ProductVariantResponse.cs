using POS.Domain.Products;

namespace POS.Application.Products;

public record ProductVariantResponse(
    Guid Id,
    Guid ProductId,
    string Sku,
    string Barcode,
    decimal PriceAmount,
    string PriceCurrency,
    PackageType PackagingType,
    int PackagingQuantity,
    decimal PackageSizeValue,
    string PackageSizeUnit,
    bool IsVatable);
