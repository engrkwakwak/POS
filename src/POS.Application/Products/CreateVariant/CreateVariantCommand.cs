using POS.Application.Abstractions.Messaging;
using POS.Domain.Products;

namespace POS.Application.Products.CreateVariant;

public record CreateVariantCommand(
    Guid ProductId,
    string Barcode,
    decimal PriceAmount,
    string PriceCurrencyCode,
    PackageType PackageType,
    int PackageQuantity,
    decimal MeasurementAmount,
    string UnitOfMeasureCode,
    bool IsVatable) : ICommand<Guid>;
