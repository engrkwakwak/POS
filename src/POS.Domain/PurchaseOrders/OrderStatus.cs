namespace POS.Domain.PurchaseOrders;

public enum OrderStatus
{
    Pending = 1,
    Submitted = 2,
    Confirmed = 3,
    Shipped = 4,
    Delivered = 5,
    Cancelled = 6
}
