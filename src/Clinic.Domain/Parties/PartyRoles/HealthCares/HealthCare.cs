using Clinic.Domain.Contracts.Parties.PartyRoles.HealthCares;

namespace Clinic.Domain.Parties.PartyRoles.HealthCares;

public class HealthCare : PartyRole
{
    public static string RoleCode = "HealthCare";
    public override string Code => RoleCode;
    public override string Title { get; protected set; }
    public HealthCare(HealthCareOptions options)
    {
        updateProperties(options);
    }

    private void updateProperties(HealthCareOptions options)
    {
        Title = options.Title;
    }
}