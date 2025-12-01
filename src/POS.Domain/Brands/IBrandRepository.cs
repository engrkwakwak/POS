using POS.Domain.Products;

namespace POS.Domain.Brands;
public interface IBrandRepository
{
    Task<bool> ExistsAsync(Guid brandId, CancellationToken cancellationToken = default);

    Task<List<Brand>> GetByNamesAsync(
        IEnumerable<string> names,
        CancellationToken cancellationToken = default);

    void Insert(Brand brand);
}
