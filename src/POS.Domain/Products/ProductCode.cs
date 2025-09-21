using System.Text.RegularExpressions;
using POS.SharedKernel;

namespace POS.Domain.Products;

public sealed record ProductCode
{
    // Regex for a 2-digit prefix, a hyphen, and a 5-digit number (e.g., 01-00001)
    private const string CodeFormatRegex = @"^\d{2}-\d{5}$";

    public string Value { get; }

    private ProductCode(string value)
    {
        Value = value;
    }

    public static Result<ProductCode> Create(string? value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return Result.Failure<ProductCode>(ProductCodeErrors.Empty);
        }

        if (!Regex.IsMatch(value, CodeFormatRegex))
        {
            return Result.Failure<ProductCode>(ProductCodeErrors.InvalidFormat);
        }

        return new ProductCode(value);
    }
}

