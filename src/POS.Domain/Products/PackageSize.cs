using POS.SharedKernel;

namespace POS.Domain.Products;

public sealed record PackageSize
{
    public decimal Value { get; }
    public SizeUnit Unit { get; }

    private PackageSize(decimal value, SizeUnit unit)
    {
        Value = value;
        Unit = unit;
    }

    public static Result<PackageSize> Create(decimal value, SizeUnit unit)
    {
        if (value <= 0)
        {
            return Result.Failure<PackageSize>(ProductErrors.PackageSize.InvalidValue);
        }

        // You can add more rules here, e.g.,
        // if (unit == SizeUnit.Gram && value > 5000) { ... }

        return new PackageSize(value, unit);
    }

    public override string ToString()
    {
        return $"{Value}{Unit.ToString().ToUpperInvariant()}"; // e.g., "500GRAM"
    }
}
