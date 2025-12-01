namespace POS.Domain.Products;
public interface IProductRepository
{
    Task<Product?> GetByIdAsync(
        Guid productId,
        CancellationToken cancellationToken = default);

    void Insert(Product product);
}
