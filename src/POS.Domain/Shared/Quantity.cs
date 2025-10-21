using POS.SharedKernel;

namespace POS.Domain.Shared;

public sealed record Quantity
{
    public Quantity(int value)
    {
        Ensure.GreaterThanZero(value);
        Value = value;
    }
    public int Value { get; }

    public static Quantity operator +(Quantity a, Quantity b) => new(a.Value + b.Value);

    public static Quantity operator -(Quantity a, Quantity b)
    {
        int newValue = a.Value - b.Value;
        if(newValue <= 0)
        {
            throw new InvalidOperationException("Resulting quantity cannot be negative");
        }
        return new Quantity(newValue);
    }

    public static Quantity Zero => new(0);

    public int CompareTo(Quantity? other)
    {
        if (other is null)
        {
            return 1;
        }

        return Value.CompareTo(other.Value);
    }

    public static bool operator >(Quantity left, Quantity right) => left.CompareTo(right) > 0;
    public static bool operator <(Quantity left, Quantity right) => left.CompareTo(right) < 0;
    public static bool operator >=(Quantity left, Quantity right) => left.CompareTo(right) >= 0;
    public static bool operator <=(Quantity left, Quantity right) => left.CompareTo(right) <= 0;
}
