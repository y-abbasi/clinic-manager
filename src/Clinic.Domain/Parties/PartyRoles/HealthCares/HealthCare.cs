using Clinic.Domain.Contracts.Parties;
using Clinic.Domain.Contracts.Parties.PartyRoles.HealthCares;
using Clinic.Domain.Parties.Organizations;

namespace Clinic.Domain.Parties.PartyRoles.HealthCares;

public class HealthCare : PartyRole
{
    public static string RoleCode = "HealthCare";
    public override string Code => RoleCode;
    public override string Title { get; protected set; }
    public override bool AcceptedByPartyType(IParty party)
    {
        return party is Organization;
    }
    public HealthCare(HealthCareOptions options)
    {
        updateProperties(options);
    }

    private void updateProperties(HealthCareOptions options)
    {
        Title = options.Title;
    }
}