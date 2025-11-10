using POS.Domain.Shared;
using POS.SharedKernel;

namespace POS.Domain.PurchaseOrders;
public sealed class PurchaseOrderItem : Entity
{
    internal PurchaseOrderItem(
        Guid id,
        Guid purchaseOrderId,
        Guid productVariantId,
        Quantity quantity,
        Money unitCost)
        : base(id)
    {
        PurchaseOrderId = purchaseOrderId;
        ProductVariantId = productVariantId;
        Quantity = quantity;
        UnitCost = unitCost;
    }

    private PurchaseOrderItem() { }

    public Guid PurchaseOrderId { get; private set; }
    public Guid ProductVariantId { get; private set; }
    public Quantity Quantity { get; private set; }
    public Money UnitCost { get; private set; }

    internal void AddQuantity(Quantity quantityToAdd)
    {
        Quantity += quantityToAdd;
    }
}
