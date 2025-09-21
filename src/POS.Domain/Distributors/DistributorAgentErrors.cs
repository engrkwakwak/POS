using POS.SharedKernel;

namespace POS.Domain.Distributors;

public static class DistributorAgentErrors
{
    public static readonly Error PrimaryContactAlreadyExists = Error.Validation(
        "DistributorAgent.PrimaryContactAlreadyExists",
        "A primary contact already exists for this agent.");
}
