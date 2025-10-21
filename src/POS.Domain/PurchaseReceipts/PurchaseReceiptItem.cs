using POS.Domain.Shared;
using POS.SharedKernel;

namespace POS.Domain.PurchaseReceipts;
public sealed class PurchaseReceiptItem : Entity
{
    internal PurchaseReceiptItem(
        Guid id,
        Guid purchaseReceiptId,
        Guid productVariantId,
        Quantity quantity,
        Money unitCost)
        : base(id)
    {
        PurchaseReceiptId = purchaseReceiptId;
        ProductVariantId = productVariantId;
        Quantity = quantity;
        UnitCost = unitCost;
    }

    private PurchaseReceiptItem() { }

    public Guid PurchaseReceiptId { get; private set; }
    public Guid ProductVariantId { get; private set; }
    public Quantity Quantity { get; private set; }
    public Money UnitCost { get; private set; }

    internal void AddQuantity(Quantity quantityToAdd)
    {
        Quantity += quantityToAdd;
    }
}
