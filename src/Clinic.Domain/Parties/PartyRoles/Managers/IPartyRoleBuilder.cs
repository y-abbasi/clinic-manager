using Clinic.Domain.Contracts.Parties.PartyRoles;
using Newtonsoft.Json.Linq;

namespace Clinic.Domain.Parties.PartyRoles.Managers;

public interface IPartyRoleBuilder
{
    string Code { get; }
    IPartyRole Build(JObject option);
}