using System.Collections.Immutable;
using Clinic.Domain.Contracts.Parties;
using Clinic.Domain.Contracts.Sessions;
using Core.Domain;

namespace Clinic.Domain.Sessions;

public class Session : AggregateRoot<SessionId>, ISession
{
    internal Session(ISessionOption option)
    {
        Id = new SessionId(option.OrganizationId, option.PractitionerId, option.Date);
    }

    public void SetAppointment(IAppointmentOption option)
    {
        
        Appointments = Appointments.Add(new Appointment(option));
    }

    private ImmutableArray<IAppointment> Appointments { get; set; } = [];
    IEnumerable<IAppointment> ISession.Appointments => Appointments;
}

public class Appointment : Entity<AppointmentId>, IAppointment
{
    public Appointment(IAppointmentOption option)
    {
        Time = option.Time;
        Patient = option.Patient;
        DurationMinute = option.DurationMinute;
    }

    public DateTime Time { get; }
    public PartyId Patient { get; }
    public int DurationMinute { get; }
}