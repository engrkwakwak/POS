using POS.SharedKernel;

namespace POS.Domain.PurchaseReceipts;
public static class PurchaseReceiptNumberErrors
{
    public static readonly Error Empty = Error.Problem(
        "PurchaseReceiptNumber.Empty",
        "Purchase receipt number cannot be empty.");
    public static readonly Error InvalidFormat = Error.Problem(
        "PurchaseReceiptNumber.InvalidFormat",
        "Purchase receipt number format is invalid. Expected format is 'PR-yyyyMMdd-####'");
}
