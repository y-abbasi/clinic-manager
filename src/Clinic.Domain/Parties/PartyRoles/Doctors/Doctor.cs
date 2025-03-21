using Clinic.Domain.Contracts.Agreements;
using Clinic.Domain.Contracts.Parties;
using Clinic.Domain.Contracts.Parties.PartyRoles.Doctors;
using Clinic.Domain.Contracts.Sessions;
using Clinic.Domain.Parties.Exceptions;
using Clinic.Domain.Parties.People;
using Clinic.Domain.Sessions.Exceptions;
using Core.SharedKernels;

namespace Clinic.Domain.Parties.PartyRoles.Doctors;

public class Doctor : PartyRole, IDoctorOptions, IAmServer
{
    public static string RoleCode => "Doctor";
    public override string Code => RoleCode;
    public override string Title { get; protected set; }
    public SpecialityType SpecialityType { get; private set; }

    public override bool ApplicableToParty(IParty party)
    {
        return party is Person;
    }

    public Doctor(IDoctorOptions options)
    {
        updateProperties(options);
    }

    private void updateProperties(IDoctorOptions options)
    {
        Title = options.Title;
        SpecialityType = options.SpecialityType;
    }

    public async Task ValidateAppointment(ISession session, IAppointmentOption appointment, IClientAppointmentService clientAppointmentService)
    {
        if (!GetValidDuration().InRange(appointment.DurationMinute))
            throw new AppointmentDurationIsInvalid();

        var appointmentDate = DateOnly.FromDateTime(appointment.Time);
        var clientAppointmentsCount = await clientAppointmentService.GetClientAppointmentsCountAsync(appointment.Patient, appointmentDate);
        
        if (clientAppointmentsCount >= 2)
            throw new ClientDailyAppointmentLimitExceeded();
    }

    private Range<int> GetValidDuration() => SpecialityType switch
    {
        SpecialityType.General => new Range<int>(10, 15),
        SpecialityType.Specialist => new Range<int>(15, 30),
        _ => throw new ArgumentOutOfRangeException()
    };
}