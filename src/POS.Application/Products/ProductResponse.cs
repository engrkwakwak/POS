using POS.Domain.Products;

namespace POS.Application.Products;

public sealed record ProductResponse
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string ProductCode { get; init; }
    public string Description { get; init; }
    public ProductCategory Category { get; init; }
    public Guid BrandId { get; init; }
    public bool IsVatable { get; init; }
}
