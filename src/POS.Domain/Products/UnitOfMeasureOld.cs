using POS.SharedKernel;

namespace POS.Domain.Products;

public sealed record UnitOfMeasureOld
{
    public UnitOfMeasureType Type { get; }
    public int? ItemsPerCase { get; }

    private UnitOfMeasureOld(UnitOfMeasureType type, int? itemsPerCase = null)
    {
        Type = type;
        ItemsPerCase = itemsPerCase;
    }

    public static Result<UnitOfMeasureOld> Create(UnitOfMeasureType type, int? itemsPerCase = null)
    {
        if (type == UnitOfMeasureType.Case && (itemsPerCase is null || itemsPerCase <= 1))
        {
            return Result.Failure<UnitOfMeasureOld>(ProductErrors.UnitOfMeasure.InvalidItemsPerCase);
        }

        if (type == UnitOfMeasureType.Piece && itemsPerCase is not null)
        {
            return Result.Failure<UnitOfMeasureOld>(ProductErrors.UnitOfMeasure.ItemsPerCaseForPieceNotAllowed);
        }

        return new UnitOfMeasureOld(type, itemsPerCase);
    }
}
