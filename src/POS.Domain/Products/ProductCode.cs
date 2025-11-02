using System.Text.RegularExpressions;
using POS.SharedKernel;

namespace POS.Domain.Products;

public sealed record ProductCode
{
    private const string Prefix = "PROD";
    private const string CodeFormatRegex = @"^" + Prefix + "-[0-9A-F]{8}$";
    private const int HashLength = 8;

    public string Value { get; }

    private ProductCode(string value)
    {
        Value = value;
    }

    public static ProductCode Generate(Guid id)
    {
        string hashPart = id.ToString("N")[..HashLength].ToUpperInvariant();
        string value = $"{Prefix}-{hashPart}";

        return new ProductCode(value);
    }

    public static Result<ProductCode> Create(string? value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return Result.Failure<ProductCode>(ProductErrors.ProductCode.Empty);
        }

        if (!Regex.IsMatch(value, CodeFormatRegex, RegexOptions.IgnoreCase))
        {
            return Result.Failure<ProductCode>(ProductErrors.ProductCode.InvalidFormat);
        }

        return new ProductCode(value.ToUpperInvariant());
    }
}
