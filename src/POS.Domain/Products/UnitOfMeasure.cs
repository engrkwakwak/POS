namespace POS.Domain.Products;

public sealed record UnitOfMeasure
{
    public static readonly UnitOfMeasure Gram = new("g", "Gram", UnitType.Mass, 1);
    public static readonly UnitOfMeasure Kilogram = new("g", "Kilogram", UnitType.Mass, 1000);
    public static readonly UnitOfMeasure Pound = new("lb", "Pound", UnitType.Mass, 453.592m);

    public static readonly UnitOfMeasure Milliliter = new("ml", "Milliliter", UnitType.Volume, 1);
    public static readonly UnitOfMeasure Liter = new("l", "Liter", UnitType.Volume, 1000);
    public static readonly UnitOfMeasure FluidOunce = new("fl oz", "Fluid Ounce", UnitType.Volume, 29.5735m);

    public string Code { get; }
    public string Name { get; }
    public UnitType Type { get; }
    public decimal ToBaseUnitMultiplier { get; }
    private UnitOfMeasure(string code, string name, UnitType type, decimal multiplier)
    {
        Code = code;
        Name = name;
        Type = type;
        ToBaseUnitMultiplier = multiplier;
    }

    public static readonly IReadOnlyCollection<UnitOfMeasure> All =
    [
        Gram, Kilogram, Pound, Milliliter, Liter, FluidOunce
    ];

    public static UnitOfMeasure FromCode(string code)
    {
        return All.FirstOrDefault(u => u.Code.Equals(code, StringComparison.OrdinalIgnoreCase))
            ?? throw new ApplicationException("The unit of measure is invalid");
    }
}
