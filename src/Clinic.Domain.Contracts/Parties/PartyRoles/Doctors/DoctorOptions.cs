namespace Clinic.Domain.Contracts.Parties.PartyRoles.Doctors;

public class DoctorOptions : IPartyRoleOptions
{
    public string Code { get; set; }
    public string Title { get; set; }
    public SpecialityType SpecialityType { get; set; }
}