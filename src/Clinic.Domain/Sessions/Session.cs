using System.Collections.Immutable;
using Clinic.Domain.Contracts.Parties;
using Clinic.Domain.Contracts.Parties.People;
using Clinic.Domain.Contracts.Sessions;
using Core.Domain;

namespace Clinic.Domain.Sessions;

public class Session : AggregateRoot<SessionId>, ISession
{
    internal Session(ISessionOption option)
    {
        Id = new SessionId(option.OrganizationId, option.PractitionerId, option.Date);
    }

    public void SetAppointment(DateTime at, IPerson patient, int durationMinute)
    {
        Appointments = Appointments.Add(new Appointment(at, patient.Id, durationMinute));
    }

    private ImmutableArray<IAppointment> Appointments { get; set; } = [];
    IEnumerable<IAppointment> ISession.Appointments => Appointments;
}

public class Appointment(DateTime time, PartyId patient, int durationMinute) : Entity<AppointmentId>, IAppointment
{
    public DateTime Time { get; } = time;
    public PartyId Patient { get; } = patient;


    public int DurationMinute { get; } = durationMinute;
}