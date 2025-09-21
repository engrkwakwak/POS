using POS.SharedKernel;

namespace POS.Domain.Products;

public sealed record Sku
{
    public string Value { get; }
    private Sku(string value) => Value = value;

    public static Result<Sku> Create(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return Result.Failure<Sku>(ProductErrors.SkuIsEmpty);
        }
        return new Sku(value);
    }
}
