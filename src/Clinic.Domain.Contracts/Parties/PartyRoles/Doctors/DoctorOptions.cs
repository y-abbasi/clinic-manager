using Clinic.Domain.Contracts.Agreements;
using Clinic.Domain.Contracts.Sessions;

namespace Clinic.Domain.Contracts.Parties.PartyRoles.Doctors;

public class DoctorOptions : IPartyRoleOptions, IDoctorOptions
{
    public string Code { get; set; }= null!;
    public string Title { get; set; }= null!;
    public SpecialityType SpecialityType { get; set; }= default!;
}

public interface IDoctorOptions
{
    public string Code { get; }
    public string Title { get; }
    public SpecialityType SpecialityType { get; }
}

public interface IAmServer
{
    Task ValidateAppointment(ISession agreement, IAppointmentOption appointment, IClientAppointmentService patientAppointmentService);
}