namespace POS.Domain.Brands;
public interface IBrandRepository
{
    Task<bool> ExistsAsync(Guid brandId, CancellationToken cancellationToken = default);
}
