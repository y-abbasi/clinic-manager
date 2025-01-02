using Clinic.Domain.Parties.PartyRoles;
using Clinic.Domain.Parties.PartyRoles.HealthCares;

namespace Clinic.Domain.Tests.Parties.PartyRoles.HealthCares;

public class HealthCareTests : PartyRoleTests<HealthCareTestBuilder, HealthCare>
{
    public HealthCareTests()
    {
        SutBuilder = CreateSutBuilder();
    }

    protected override HealthCareTestBuilder CreateSutBuilder() => new HealthCareTestBuilder();
    private HealthCareTestBuilder SutBuilder;
}