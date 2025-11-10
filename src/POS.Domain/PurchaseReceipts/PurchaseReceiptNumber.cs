using System.Text.RegularExpressions;
using POS.SharedKernel;

namespace POS.Domain.PurchaseReceipts;

public sealed partial record PurchaseReceiptNumber
{
    private const string FormatPattern = @"^PR-\d{8}-\d{4}$";

    public string Value { get; }

    private PurchaseReceiptNumber(string value) => Value = value;

    public static Result<PurchaseReceiptNumber> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Result.Failure<PurchaseReceiptNumber>(PurchaseReceiptNumberErrors.Empty);
        }

        if (!PurchaseReceiptNumberRegex().IsMatch(value))
        {
            return Result.Failure<PurchaseReceiptNumber>(PurchaseReceiptNumberErrors.InvalidFormat);
        }
        return new PurchaseReceiptNumber(value);
    }


    [GeneratedRegex(FormatPattern)]
    private static partial Regex PurchaseReceiptNumberRegex();
}
