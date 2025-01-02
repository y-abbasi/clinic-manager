using Clinic.Domain.Contracts.Parties.PartyRoles;

namespace Clinic.Domain.Contracts.Parties;

public interface IPartyOptions
{
    IEnumerable<IPartyRoleOptions> PartyRoles { get; }
}