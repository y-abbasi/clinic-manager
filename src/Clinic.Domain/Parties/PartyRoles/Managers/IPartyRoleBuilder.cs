using Clinic.Domain.Contracts.Parties.PartyRoles;
using Newtonsoft.Json.Linq;

namespace Clinic.Domain.Parties.PartyRoles.Managers;

public interface IPartyRoleBuilder
{
    string Code { get; }
    Type GetPartyRoleType();
    Type GetPartyRoleOptionType();
}