using POS.SharedKernel;

namespace POS.Domain.PurchaseOrders;
public sealed class PurchaseOrderService
{
    private readonly IOrderNumberGenerator _orderNumberGenerator;
    private readonly IDateTimeProvider _dateTimeProvider;

    public PurchaseOrderService(
        IOrderNumberGenerator orderNumberGenerator,
        IDateTimeProvider dateTimeProvider)
    {
        _orderNumberGenerator = orderNumberGenerator;
        _dateTimeProvider = dateTimeProvider;
    }

    // Sample implementation of creating a new PurchaseOrder and generating its order number
    public async Task<Result<PurchaseOrder>> CreateAsync()
    {
        PurchaseOrderNumber orderNumber = await _orderNumberGenerator.GenerateNextAsync();
        DateTime orderedDateInUtc = _dateTimeProvider.UtcNow;

        var purchaseOrder = PurchaseOrder.Create(
            distributorAgentId: Guid.Empty,
            orderNumber: orderNumber,
            branchId:  Guid.Empty,
            orderedByUserId: Guid.Empty,
            orderedDateInUtc: orderedDateInUtc);
        return purchaseOrder;
    }
}
