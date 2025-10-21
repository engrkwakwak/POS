using System.Text.RegularExpressions;
using POS.SharedKernel;

namespace POS.Domain.PurchaseOrders;

public sealed partial record PurchaseOrderNumber
{
    private const string FormatPattern = @"^PO-\d{8}-\d{4}$";

    public string Value { get; }
    
    private PurchaseOrderNumber(string value) => Value = value;

    public static Result<PurchaseOrderNumber> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Result.Failure<PurchaseOrderNumber>(PurchaseOrderNumberErrors.Empty);
        }

        if (!PurchaseOrderNumberRegex().IsMatch(value))
        {
            return Result.Failure<PurchaseOrderNumber>(PurchaseOrderNumberErrors.InvalidFormat);
        }

        return new PurchaseOrderNumber(value);
    }

    [GeneratedRegex(FormatPattern)]
    private static partial Regex PurchaseOrderNumberRegex();
}
