using System.Text.RegularExpressions;
using POS.SharedKernel;

namespace POS.Domain.Products;

public sealed record Sku
{
    private const string Prefix = "SKU";
    private const string CodeFormatRegex = $@"^{Prefix}-[0-9A-F]{{8}}$";
    private const int HashLength = 8;

    public string Value { get; }
    private Sku(string value) => Value = value;

    public static Sku Generate(Guid id)
    {
        string hashPart = id.ToString("N")[..HashLength].ToUpperInvariant();
        string value = $"{Prefix}-{hashPart}";

        return new Sku(value);
    }

    public static Result<Sku> Create(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return Result.Failure<Sku>(ProductErrors.Sku.Empty);
        }

        if (!Regex.IsMatch(value, CodeFormatRegex, RegexOptions.IgnoreCase))
        {
            return Result.Failure<Sku>(ProductErrors.Sku.InvalidFormat);
        }

        return new Sku(value);
    }
}
