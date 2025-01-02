using Clinic.Domain.Parties.PartyRoles.HealthCares;

namespace Clinic.Domain.Tests.Parties.PartyRoles.HealthCares;

public abstract class HealthCareTestBuilder<TSelf> : PartyRoleTestBuilder<TSelf, HealthCare>
    where TSelf: class, IPartyRoleTestBuilder<TSelf, HealthCare>
{
    public override string Code => HealthCare.RoleCode;
}

public class HealthCareTestBuilder : HealthCareTestBuilder<HealthCareTestBuilder>
{
    public HealthCareTestBuilder()
    {
        WithTitle(TestConstants.SomeName);
    }
}