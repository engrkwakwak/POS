using POS.SharedKernel;

namespace POS.Domain.Products;

public sealed record Measurement
{
    public decimal Value { get; }
    public UnitOfMeasure Unit { get; }

    private Measurement(decimal value, UnitOfMeasure unit)
    {
        Value = value;
        Unit = unit;
    }

    public static Result<Measurement> Create(decimal value, UnitOfMeasure unit)
    {
        if (value <= 0)
        {
            return Result.Failure<Measurement>(ProductErrors.Measurement.InvalidValue);
        }

        return new Measurement(value, unit);
    }

    public decimal GetBaseValue()
    {
        return Value * Unit.ToBaseUnitMultiplier;
    }

    public override string ToString() => $"{Value} {Unit.Code}";
}
