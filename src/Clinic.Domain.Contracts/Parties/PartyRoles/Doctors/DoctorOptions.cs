namespace Clinic.Domain.Contracts.Parties.PartyRoles.Doctors;

public class DoctorOptions : IPartyRoleOptions
{
    public string Code { get; set; }= default!;
    public string Title { get; set; }= default!;
    public SpecialityType SpecialityType { get; set; }= default!;
}