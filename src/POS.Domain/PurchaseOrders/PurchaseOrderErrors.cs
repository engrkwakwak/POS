using POS.SharedKernel;

namespace POS.Domain.PurchaseOrders;
public static class PurchaseOrderErrors
{
    public static readonly Error NotPending = Error.Problem(
        "PurchaseOrder.NotPending",
        "The purchase order is not in a pending state and cannot be modified.");
}
