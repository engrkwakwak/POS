using POS.SharedKernel;

namespace POS.Domain.Brands;
public sealed record BrandCreatedDomainEvent(Guid BrandId) : IDomainEvent;
