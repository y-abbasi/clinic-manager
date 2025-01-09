using Clinic.Domain.Contracts.Parties;
using Clinic.Domain.Contracts.Parties.PartyRoles;
using Clinic.Domain.Contracts.Parties.PartyRoles.Doctors;
using Clinic.Domain.Parties.People;

namespace Clinic.Domain.Parties.PartyRoles.Doctors;

public class Doctor :PartyRole
{
    public static string RoleCode => "Doctor";
    public override string Code => RoleCode;
    public override string Title { get; protected set; }
    public SpecialityType SpecialityType { get; private set; }
    public override bool ApplicableToParty(IParty party)
    {
        return party is Person;
    }

    public Doctor(DoctorOptions options)
    {
        updateProperties(options);
    }

    private void updateProperties(DoctorOptions options)
    {
        Title = options.Title;
        SpecialityType = options.SpecialityType;
    }
}