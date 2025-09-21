using POS.SharedKernel;

namespace POS.Domain.Products;

public sealed record Barcode
{
    public Barcode(string? value)
    {
        Ensure.NotNullOrEmpty(value);

        Value = value;
    }

    public string Value { get; }
}
