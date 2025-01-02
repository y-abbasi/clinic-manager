using Clinic.Domain.Parties.PartyRoles.Doctors;

namespace Clinic.Domain.Tests.Parties.PartyRoles.Doctors;

public abstract class DoctorTestBuilder<TSelf> : PartyRoleTestBuilder<TSelf, Doctor>
    where TSelf: class, IPartyRoleTestBuilder<TSelf, Doctor>
{
    public override string Code => Doctor.RoleCode;
}
public class DoctorTestBuilder : DoctorTestBuilder<DoctorTestBuilder>
{
    public DoctorTestBuilder()
    {
        WithTitle(TestConstants.SomeName);
    }
}