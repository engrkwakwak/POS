namespace POS.Domain.Products;

public sealed record Description
{

    public Description(string? value)
    {
        Value = value;
    }

    public string? Value { get; }
}
