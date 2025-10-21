using POS.Domain.PurchaseOrders;
using POS.Domain.Shared;
using POS.SharedKernel;

namespace POS.Domain.PurchaseReceipts;
public sealed class PurchaseReceipt : Entity
{
    private PurchaseReceipt(
        Guid id,
        PurchaseReceiptNumber receiptNumber,
        Guid purchaseOrderId,
        Guid receivedByUserId,
        DateTime receiptDateInUtc,
        PurchaseReceiptStatus purchaseReceiptStatus)
        : base(id)
    {
        PurchaseReceiptNumber = receiptNumber;
        PurchaseOrderId = purchaseOrderId;
        ReceivedByUserId = receivedByUserId;
        ReceiptDateInUtc = receiptDateInUtc;
        PurchaseReceiptStatus = purchaseReceiptStatus;
    }

    private PurchaseReceipt() { }

    public PurchaseReceiptNumber PurchaseReceiptNumber { get; private set; }
    public Guid PurchaseOrderId { get; private set; }
    public Guid ReceivedByUserId { get; private set; }
    public DateTime ReceiptDateInUtc { get; private set; }
    public PurchaseReceiptStatus PurchaseReceiptStatus { get; private set; }

    private readonly List<PurchaseReceiptItem> _receiptItems = [];
    public IReadOnlyCollection<PurchaseReceiptItem> ReceiptItems => _receiptItems.AsReadOnly();

    public static async Task<Result<PurchaseReceipt>> Create(
        Guid purchaseOrderId,
        Guid receivedByUserId,
        DateTime receiptDateInUtc,
        IDocumentNumberGenerationService documentNumberGenerationService)
    {
        PurchaseReceiptNumber receiptNumber = await documentNumberGenerationService
            .GeneratePurchaseReceiptNumberAsync();

        var purchaseReceipt = new PurchaseReceipt(
            Guid.NewGuid(),
            receiptNumber,
            purchaseOrderId,
            receivedByUserId,
            receiptDateInUtc,
            purchaseReceiptStatus: PurchaseReceiptStatus.Pending);
        return Result.Success(purchaseReceipt);
    }

    public Result AddReceiptItem(
        PurchaseOrderItem orderItem,
        Quantity quantityToReceive,
        Quantity quantityAlreadyReceived)
    {
        if (PurchaseReceiptStatus != PurchaseReceiptStatus.Pending)
        {
            return Result.Failure(PurchaseReceiptErrors.NotPending);
        }

        if (quantityToReceive <= Quantity.Zero)
        {
            return Result.Failure(PurchaseReceiptErrors.InvalidQuantity);
        }

        PurchaseReceiptItem? existingItemInThisReceipt = _receiptItems
            .FirstOrDefault(ri => ri.ProductVariantId == orderItem.ProductVariantId);
        Quantity quantityInThisReceipt = existingItemInThisReceipt?.Quantity ?? Quantity.Zero;

        if(quantityToReceive + quantityInThisReceipt + quantityAlreadyReceived > orderItem.Quantity)
        {
            return Result.Failure(PurchaseReceiptErrors.OverReceiving);
        }

        if (existingItemInThisReceipt != null)
        {
            existingItemInThisReceipt.AddQuantity(quantityToReceive);
        }
        else
        {
            var receiptItem = new PurchaseReceiptItem(
                Guid.NewGuid(),
                Id,
                orderItem.ProductVariantId,
                quantityToReceive,
                orderItem.UnitCost);
            _receiptItems.Add(receiptItem);
        }

        return Result.Success();
    }
}
