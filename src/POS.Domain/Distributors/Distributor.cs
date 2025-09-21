using POS.Domain.Brands;
using POS.Domain.Shared;
using POS.SharedKernel;

namespace POS.Domain.Distributors;

public sealed class Distributor : Entity
{
    private Distributor(Guid id, Name name)
        : base(id)
    {
        Name = name;
    }

    private Distributor()
    {
    }

    public Name Name { get; private set; }

    private readonly List<Brand> _brands = [];
    public IReadOnlyCollection<Guid> BrandIds => _brands.Select(b => b.Id).ToList().AsReadOnly();

    private readonly List<DistributorAgent> _agents = [];
    public IReadOnlyCollection<DistributorAgent> Agents => _agents.AsReadOnly();

    public static Distributor Create(Name name)
    {
        var distributor = new Distributor(Guid.NewGuid(), name);

        distributor.Raise(new DistributorCreatedDomainEvent(distributor.Id));

        return distributor;
    }

    public void AssignBrand(Brand brand)
    {
        if (_brands.All(b => b.Id != brand.Id))
        {
            _brands.Add(brand);
        }
    }

    public Result<DistributorAgent> AddAgent(
        FirstName firstName,
        LastName lastName,
        Email email,
        Notes notes)
    {
        if (_agents.Any(a => a.Email == email))
        {
            // Return a specific error
            return Result.Failure<DistributorAgent>(DistributorErrors.AgentWithEmailAlreadyExists);
        }

        // Create the new agent with all the required info
        var newAgent = new DistributorAgent(
            Guid.NewGuid(),
            Id,
            firstName,
            lastName,
            email,
            Status.Active,
            notes);

        _agents.Add(newAgent);

        return newAgent;
    }
}
