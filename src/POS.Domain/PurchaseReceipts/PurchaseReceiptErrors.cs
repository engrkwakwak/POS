using System.Net.NetworkInformation;
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
        "The quantity to receive exceeds the quantity ordered.");

    public static readonly Error InvalidPurchaseOrderId = Error.Problem(
        "PurchaseReceipt.InvalidPurchaseOrderId",
        "The provided purchase order item is invalid or does not specify a product.");

    public static readonly Error InvalidUserId = Error.Problem(
        "PurchaseReceipt.InvalidUserId",
        "The provided user ID for receiving is invalid.");

    public static readonly Error NullOrderItem = Error.Problem(
        "PurchaseReceipt.NullOrderItem",
        "Purchase order item cannot be null.");

    public static readonly Error OrderMismatch = Error.Problem(
        "PurchaseReceipt.OrderMismatch",
        "Order item does not belong to this receipt's purchase order.");
}
