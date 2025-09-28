using POS.SharedKernel;

namespace POS.Domain.Distributors;

public static class DistributorErrors
{
    public static readonly Error AgentWithEmailAlreadyExists = Error.Problem(
        "Distributor.AgentWithEmailAlreadyExists",
        "An agent with the provided email already exists for this distributor.");
}
