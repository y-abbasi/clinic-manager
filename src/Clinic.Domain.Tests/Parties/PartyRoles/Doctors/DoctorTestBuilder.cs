using Clinic.Domain.Contracts.Parties.PartyRoles.Doctors;
using Clinic.Domain.Parties.PartyRoles.Doctors;

namespace Clinic.Domain.Tests.Parties.PartyRoles.Doctors;

public abstract class DoctorTestBuilder<TSelf> : PartyRoleTestBuilder<TSelf, Doctor>, IDoctorOptions
    where TSelf : class, IPartyRoleTestBuilder<TSelf, Doctor>
{
    public override string Code => Doctor.RoleCode;
    public SpecialityType SpecialityType { get; set; }

    public TSelf WithSpecialityType(SpecialityType specialityType)
    {
        SpecialityType = specialityType;
        return this as TSelf;
    }
}

public class DoctorTestBuilder : DoctorTestBuilder<DoctorTestBuilder>
{
    public DoctorTestBuilder()
    {
        WithTitle(TestConstants.SomeName);
    }

}