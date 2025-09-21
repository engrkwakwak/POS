using POS.SharedKernel;

namespace POS.Domain.Products;
public static class ProductCodeErrors
{
    public static readonly Error Empty = Error.Validation(
        "ProductCode.Empty", 
        "Product code cannot be empty.");

    public static readonly Error InvalidFormat = Error.Validation(
        "ProductCode.InvalidFormat",
        "The product code format is invalid. It must be a 2-digit prefix, a hyphen, and a 5-digit number (e.g., 01-00001).");
}
