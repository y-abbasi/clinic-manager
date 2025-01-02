using Clinic.Domain.Contracts.Parties.PartyRoles;

namespace Clinic.Domain.Parties.PartyRoles;

public abstract class PartyRole : IPartyRole
{
    public abstract string Code { get; }
    public abstract string Title { get; protected set; }
}