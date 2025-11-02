using POS.SharedKernel;

namespace POS.Domain.Products;

public sealed record UnitOfMeasure
{
    public UnitOfMeasureType Type { get; }
    public int? ItemsPerCase { get; }

    private UnitOfMeasure(UnitOfMeasureType type, int? itemsPerCase = null)
    {
        Type = type;
        ItemsPerCase = itemsPerCase;
    }

    public static Result<UnitOfMeasure> Create(UnitOfMeasureType type, int? itemsPerCase = null)
    {
        if (type == UnitOfMeasureType.Case && (itemsPerCase is null || itemsPerCase <= 1))
        {
            return Result.Failure<UnitOfMeasure>(ProductErrors.UnitOfMeasure.InvalidItemsPerCase);
        }

        if (type == UnitOfMeasureType.Piece && itemsPerCase is not null)
        {
            return Result.Failure<UnitOfMeasure>(ProductErrors.UnitOfMeasure.ItemsPerCaseForPieceNotAllowed);
        }

        return new UnitOfMeasure(type, itemsPerCase);
    }
}
