

using POS.SharedKernel;

namespace POS.Domain.PurchaseReceipts;
public static class PurchaseReceiptErrors
{
    public static readonly Error InvalidQuantity = Error.Problem(
        "PurchaseReceipt.InvalidQuantity",
        "The quantity for product must be greater than zero.");

    public static readonly Error NotPending = Error.Problem(
        "PurchaseReceipt.NotPending",
        "The purchase receipt is not in a pending state and cannot be modified.");

    public static readonly Error OverReceiving = Error.Problem(
        "PurchaseReceipt.OverReceiving",
        "The quantity to receive exceeds the quantity ordered");
}
