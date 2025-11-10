using POS.Domain.Shared;
using POS.SharedKernel;

namespace POS.Domain.PurchaseOrders;
public sealed class PurchaseOrder : Entity
{
    private PurchaseOrder(
        Guid id,
        PurchaseOrderNumber orderNumber,
        Guid distributorAgentId,
        Guid branchId,
        Guid orderedByUserId,
        DateTime orderDateInUtc)
        : base(id)
    {
        PurchaseOrderNumber = orderNumber;
        DistributorAgentId = distributorAgentId;
        BranchId = branchId;
        OrderedByUserId = orderedByUserId;
        OrderDateInUtc = orderDateInUtc;
        Status = OrderStatus.Pending;
    }

    private PurchaseOrder() { }

    public PurchaseOrderNumber PurchaseOrderNumber { get; private set; }
    public Guid DistributorAgentId { get; private set; }
    public Guid BranchId { get; private set; }
    public Guid OrderedByUserId { get; private set; }
    public DateTime OrderDateInUtc { get; private set; }
    public OrderStatus Status { get; private set; }

    private readonly List<PurchaseOrderItem> _orderItems = [];
    public IReadOnlyCollection<PurchaseOrderItem> Items => _orderItems.AsReadOnly();

    public static PurchaseOrder Create(
        Guid distributorAgentId,
        PurchaseOrderNumber orderNumber,
        Guid branchId,
        Guid orderedByUserId,
        DateTime orderedDateInUtc)
    {
        var purchaseOrder = new PurchaseOrder(
            Guid.NewGuid(),
            orderNumber,
            distributorAgentId,
            branchId,
            orderedByUserId,
            orderedDateInUtc);

        return purchaseOrder;
    }

    public Result AddOrderItem(
        Guid productVariantId,
        Quantity quantity,
        Money unitCost)
    {
        if (Status != OrderStatus.Pending)
        {
            return Result.Failure(PurchaseOrderErrors.NotPending);
        }

        PurchaseOrderItem existingItem = _orderItems
            .FirstOrDefault(i => i.ProductVariantId == productVariantId);
        if (existingItem != null)
        {
            existingItem.AddQuantity(quantity);
            return Result.Success();
        }

        var orderItem = new PurchaseOrderItem(
            Guid.NewGuid(),
            Id,
            productVariantId,
            quantity,
            unitCost);
        _orderItems.Add(orderItem);

        return Result.Success();
    }
}
