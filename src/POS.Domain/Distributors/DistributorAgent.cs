using POS.SharedKernel;

namespace POS.Domain.Distributors;
public sealed class DistributorAgent : Entity
{
    internal DistributorAgent(
        Guid id, 
        Guid distributorId, 
        FirstName firstName,
        LastName lastName,
        Email email,
        Status status,
        Notes notes
        )
        : base(id)
    {
        DistributorId = distributorId;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Status = status;
        Notes = notes;
    }

    private DistributorAgent()
    {
    }

    public Guid DistributorId { get; private set; }
    public FirstName FirstName { get; private set; }
    public LastName LastName { get; private set; }
    public Email Email { get; private set; }
    public Status Status { get; private set; }
    public Notes Notes { get; private set; }

    private readonly List<AgentContact> _contacts = [];
    public IReadOnlyCollection<AgentContact> Contacts => _contacts.AsReadOnly();

    public Result AddContact(
        ContactType contactType,
        string number,
        string regionCode,
        bool isPrimary = false)
    {
        Result<PhoneNumber> phoneNumberResult = PhoneNumber.Create(number, regionCode);

        if (phoneNumberResult.IsFailure)
        {
            return phoneNumberResult;
        }

        if (isPrimary && _contacts.Any(c => c.IsPrimary))
        {
            return Result.Failure(DistributorAgentErrors.PrimaryContactAlreadyExists);
        }

        var agentContact = new AgentContact(
            Guid.NewGuid(),
            Id,
            contactType,
            phoneNumberResult.Value,
            isPrimary);

        _contacts.Add(agentContact);

        return Result.Success();
    }
}
