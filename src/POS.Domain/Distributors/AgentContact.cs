using POS.SharedKernel;

namespace POS.Domain.Distributors;
public sealed class AgentContact : Entity
{
    internal AgentContact(
        Guid id,
        Guid agentId,
        ContactType contactType,
        PhoneNumber phoneNumber,
        bool isPrimary)
        : base(id)
    {
        AgentId = agentId;
        ContactType = contactType;
        PhoneNumber = phoneNumber;
        IsPrimary = isPrimary;
    }

    private AgentContact()
    {
    }

    public Guid AgentId { get; private set; }
    public ContactType ContactType { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public bool IsPrimary { get; private set; }
}
