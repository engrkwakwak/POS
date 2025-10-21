using POS.Domain.Shared;
using POS.SharedKernel;

namespace POS.Domain.Products;
public sealed class Product : Entity
{
    private readonly List<ProductVariant> _variants = [];

    private Product(
        Guid id,
        ProductCode productCode, 
        Name name, 
        Description description, 
        ProductCategory category, 
        Guid brandId, 
        bool isVatable)
        : base(id)
    {
        ProductCode = productCode;
        Name = name;
        Description = description;
        Category = category;
        BrandId = brandId;
        IsVatable = isVatable;
    }

    private Product()
    {
    }

    public ProductCode ProductCode { get; private set; }
    public Name Name { get; private set; }
    public Description Description { get; private set; }
    public ProductCategory Category { get; private set; }
    public Guid BrandId { get; private set; }
    public bool IsVatable { get; private set; }
    public IReadOnlyCollection<ProductVariant> Variants => _variants.AsReadOnly();

    public static Result<Product> Create(
        ProductCode productCode,
        Name name,
        Description description,
        ProductCategory category,
        Guid brandId,
        bool isVatable)
    {
        var product = new Product(
            Guid.NewGuid(),
            productCode,
            name,
            description,
            category,
            brandId,
            isVatable);
        return Result.Success(product);
    }

    public Result<ProductVariant> AddVariant(
        Barcode barcode,
        Money price,
        UnitOfMeasure unitOfMeasure,
        bool isVatable)
    {
        if (_variants.Any(v => v.Barcode == barcode))
        {
            return Result.Failure<ProductVariant>(ProductErrors.VariantWithBarcodeAlreadyExists);
        }

        // Put this inside the domain service later.
        int nextVariantSuffix = _variants.Count + 1;
        string skuValue = $"{ProductCode.Value}-{nextVariantSuffix:D2}";
        Result<Sku> skuResult = Sku.Create(skuValue);

        if (skuResult.IsFailure)
        {
            return Result.Failure<ProductVariant>(skuResult.Error);
        }

        var variant = new ProductVariant(
            Guid.NewGuid(), 
            Id, 
            skuResult.Value, 
            barcode, 
            price, 
            unitOfMeasure, 
            isVatable);

        _variants.Add(variant);

        return variant;
    }
}
