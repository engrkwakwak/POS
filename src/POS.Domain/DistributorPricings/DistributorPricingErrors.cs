using POS.SharedKernel;

namespace POS.Domain.DistributorPricings;

public static class DistributorPricingErrors
{
    public static readonly Error CostMustBePositive = Error.Validation(
        "DistributorPricing.CostMustBePositive",
        "The cost for a product must be a positive value.");
}
