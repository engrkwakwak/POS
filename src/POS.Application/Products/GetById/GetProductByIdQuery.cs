using POS.Application.Abstractions.Caching;

namespace POS.Application.Products.GetById;

public sealed record GetProductByIdQuery(Guid ProductId) 
    : ICachedQuery<ProductResponse>
{
    public string CacheKey => $"pos:product:{ProductId}";

    public TimeSpan? Expiration => TimeSpan.FromHours(24);
}
