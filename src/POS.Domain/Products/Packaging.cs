using POS.SharedKernel;

namespace POS.Domain.Products;

public sealed record Packaging
{
    public PackageType Type { get; }
    public int Quantity { get; }

    private Packaging(PackageType type, int quantity)
    {
        Type = type;
        Quantity = quantity;
    }

    public static Packaging Piece()
    {
        return new Packaging(PackageType.Piece, 1);
    }

    public static Result<Packaging> Box(int quantityPerBox)
    {
        if (quantityPerBox <= 1)
        {
            return Result.Failure<Packaging>(ProductErrors.Packaging.InvalidBoxQuantity);
        }

        return new Packaging(PackageType.Box, quantityPerBox);
    }

    public int TotalItems(int numberOfBoxes) => Quantity * numberOfBoxes;

    public override string ToString() => Type == PackageType.Piece
        ? "Piece"
        : $"Box of {Quantity}";
}
