namespace POS.Domain.Products;

public interface IProductVariantRepository
{
    Task<bool> IsBarcodeUniqueAsync(
        string barcode,
        CancellationToken cancellationToken = default);
}
