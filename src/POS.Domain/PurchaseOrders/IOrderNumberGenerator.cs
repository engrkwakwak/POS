namespace POS.Domain.PurchaseOrders;
public interface IOrderNumberGenerator
{
    Task<PurchaseOrderNumber> GenerateNextAsync();
}
