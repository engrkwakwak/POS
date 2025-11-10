using POS.Domain.Shared;
using POS.SharedKernel;

namespace POS.Domain.Products;
public sealed class Product : Entity
{
    private readonly List<ProductVariant> _variants = [];

    private Product(
        Guid id,
        Name name, 
        Description description, 
        ProductCategory category, 
        Guid brandId, 
        bool isVatable)
        : base(id)
    {
        ProductCode = ProductCode.Generate(id);
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
        Name name,
        Description description,
        ProductCategory category,
        Guid brandId,
        bool isVatable)
    {
        var product = new Product(
            Guid.NewGuid(),
            name,
            description,
            category,
            brandId,
            isVatable);

        return product;
    }

    public Result<ProductVariant> AddVariant(
        Barcode barcode,
        Money price,
        UnitOfMeasure unitOfMeasure,
        PackageSize size,
        bool isVatable)
    {
        if (_variants.Any(v => v.Barcode == barcode))
        {
            return Result.Failure<ProductVariant>(ProductErrors.VariantWithBarcodeAlreadyExists);
        }

        var variant = new ProductVariant(
            Guid.NewGuid(), 
            Id, 
            barcode, 
            price, 
            unitOfMeasure,
            size,
            isVatable);

        _variants.Add(variant);

        return Result.Success(variant);
    }
}
