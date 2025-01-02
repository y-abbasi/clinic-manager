using Clinic.Domain.Parties.PartyRoles.Doctors;
using Clinic.Domain.Tests.Parties.PartyRoles.HealthCares;

namespace Clinic.Domain.Tests.Parties.PartyRoles.Doctors;

public class DoctorTests : PartyRoleTests<DoctorTestBuilder, Doctor>
{
    public DoctorTests()
    {
        SutBuilder = CreateSutBuilder();
    }

    protected override DoctorTestBuilder CreateSutBuilder() => new();
    private DoctorTestBuilder SutBuilder;
}