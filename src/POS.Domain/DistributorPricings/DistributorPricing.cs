using POS.Domain.Shared;
using POS.SharedKernel;

namespace POS.Domain.DistributorPricings;
public sealed class DistributorPricing : Entity
{
    private DistributorPricing(
        Guid id,
        Guid distributorId,
        Guid productVariantId,
        Money cost,
        DateTime lastUpdatedAtUtc)
        : base(id)
    {
        DistributorId = distributorId;
        ProductVariantId = productVariantId;
        Cost = cost;
        LastUpdatedAtUtc = lastUpdatedAtUtc;
    }
    private DistributorPricing() { }

    public Guid DistributorId { get; private set; }
    public Guid ProductVariantId { get; private set; }
    public Money Cost {  get; private set; }
    public DateTime LastUpdatedAtUtc { get; private set; }

    public static Result<DistributorPricing> Create(
        Guid distributorId,
        Guid productVariantId,
        Money cost,
        DateTime lastUpdatedAtUtc)
    {
        if (cost.Amount < 0)
        {
            return Result.Failure<DistributorPricing>(DistributorPricingErrors.CostMustBePositive);
        }

        var pricing = new DistributorPricing(
            Guid.NewGuid(),
            distributorId,
            productVariantId,
            cost,
            lastUpdatedAtUtc);

        return pricing;
    }

    public Result UpdateCost(Money newCost)
    {
        if (newCost.Amount <= 0)
        {
            return Result.Failure<DistributorPricing>(DistributorPricingErrors.CostMustBePositive);
        }

        Cost = newCost;

        return Result.Success();
    }
}
