using POS.Application.Abstractions.Caching;

namespace POS.Application.Products.GetVariantById;

public sealed record GetVariantByIdQuery(Guid ProductId, Guid ProductVariantId)
    : ICachedQuery<ProductVariantResponse>
{
    public string CacheKey => $"pos:product-variant:{ProductVariantId}";

    public TimeSpan? Expiration => TimeSpan.FromHours(24);
}
