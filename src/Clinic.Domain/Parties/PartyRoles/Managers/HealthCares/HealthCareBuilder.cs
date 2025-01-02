using Clinic.Domain.Contracts.Parties.PartyRoles;
using Clinic.Domain.Contracts.Parties.PartyRoles.HealthCares;
using Clinic.Domain.Parties.PartyRoles.HealthCares;
using Newtonsoft.Json.Linq;

namespace Clinic.Domain.Parties.PartyRoles.Managers.HealthCares;

class HealthCareBuilder : IPartyRoleBuilder
{
    public string Code { get; } = HealthCare.RoleCode;
    public Type GetPartyRoleType() => typeof(HealthCare);

    public Type GetPartyRoleOptionType() => typeof(HealthCareOptions);
}