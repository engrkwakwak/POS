using POS.SharedKernel;

namespace POS.Domain.PurchaseOrders;
public static class PurchaseOrderNumberErrors
{
    public static readonly Error Empty = Error.Problem(
        "PurchaseOrderNumber.Empty",
        "Purchase order number cannot be empty.");

    public static readonly Error InvalidFormat = Error.Problem(
        "PurchaseOrderNumber.InvalidFormat",
        "Purchase order number format is invalid. Expected format is 'PO-yyyyMMdd-####'");
}
