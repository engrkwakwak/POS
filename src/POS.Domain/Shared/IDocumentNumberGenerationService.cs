using POS.Domain.PurchaseOrders;
using POS.Domain.PurchaseReceipts;

namespace POS.Domain.Shared;
public interface IDocumentNumberGenerationService
{
    Task<PurchaseOrderNumber> GeneratePurchaseOrderNumberAsync();
    Task<PurchaseReceiptNumber> GeneratePurchaseReceiptNumberAsync();
}
