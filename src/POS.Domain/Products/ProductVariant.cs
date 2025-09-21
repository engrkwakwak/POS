using POS.Domain.Shared;
using POS.SharedKernel;

namespace POS.Domain.Products;
public sealed class ProductVariant : Entity
{
    internal ProductVariant(
        Guid id,
        Guid productId,
        Sku sku,
        Barcode barcode,
        Money price,
        UnitOfMeasure unitOfMeasure,
        bool isVatable)
        : base(id)
    {
        ProductId = productId;
        Sku = sku;
        Barcode = barcode;
        Price = price;
        UnitOfMeasure = unitOfMeasure;
        IsVatable = isVatable;
    }

    private ProductVariant()
    {
    }

    public Guid ProductId { get; private set; }
    public Sku Sku { get; private set; }
    public Barcode Barcode { get; private set; }
    public Money Price { get; private set; }
    public UnitOfMeasure UnitOfMeasure { get; private set; }
    public bool IsVatable { get; private set; }
}
