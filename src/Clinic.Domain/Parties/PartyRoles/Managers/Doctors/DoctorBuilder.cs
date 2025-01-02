using Clinic.Domain.Contracts.Parties.PartyRoles;
using Clinic.Domain.Contracts.Parties.PartyRoles.Doctors;
using Clinic.Domain.Parties.PartyRoles.Doctors;
using Newtonsoft.Json.Linq;

namespace Clinic.Domain.Parties.PartyRoles.Managers.Doctors;

class DoctorBuilder : IPartyRoleBuilder
{
    public string Code { get; } = Doctor.RoleCode;

    public Type GetPartyRoleType()
    {
        return typeof(Doctor);
    }

    public Type GetPartyRoleOptionType() => typeof(DoctorOptions);
}