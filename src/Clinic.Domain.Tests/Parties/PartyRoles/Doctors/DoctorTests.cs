using Clinic.Domain.Parties.PartyRoles.Doctors;

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