using POS.Domain.Shared;
using POS.SharedKernel;

namespace POS.Domain.Products;
public sealed class ProductVariant : Entity
{
    internal ProductVariant(
        Guid id,
        Guid productId,
        Barcode barcode,
        Money price,
        Packaging packaging,
        Measurement packageSize,
        bool isVatable)
        : base(id)
    {
        ProductId = productId;
        Sku = Sku.Generate(id);
        Barcode = barcode;
        Price = price;
        Packaging = packaging;
        PackageSize = packageSize;
        IsVatable = isVatable;
    }

    private ProductVariant()
    {
    }

    public Guid ProductId { get; private set; }
    public Sku Sku { get; private set; }
    public Barcode Barcode { get; private set; }
    public Money Price { get; private set; }
    public Packaging Packaging { get; private set; }
    public Measurement PackageSize { get; private set; }

    public bool IsVatable { get; private set; }
}
