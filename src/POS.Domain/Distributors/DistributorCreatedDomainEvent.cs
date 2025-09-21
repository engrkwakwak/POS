using POS.SharedKernel;

namespace POS.Domain.Distributors;

public sealed record DistributorCreatedDomainEvent(Guid DistributorId) : IDomainEvent;
