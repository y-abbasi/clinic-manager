using Clinic.Domain.Contracts.Parties;
using Clinic.Domain.Contracts.Parties.People;
using Core.Domain;

namespace Clinic.Domain.Contracts.Sessions;

public interface ISession : IAggregateRoot<SessionId>
{
    void SetAppointment(DateTime at, IPerson patient, int durationMinute);
    IEnumerable<IAppointment> Appointments { get; }
}

public interface IAppointment : IEntity<AppointmentId>
{
    DateTime Time { get; }
    PartyId Patient { get; }
    int DurationMinute { get; }
}

public record SessionId(PartyId OrganizationId, PartyId PractitionerId, DateOnly Date);

public record AppointmentId(Guid Value)
{
    public AppointmentId() : this(Guid.NewGuid())
    {
    }
}

public interface ISessionOption
{
    PartyId OrganizationId { get; }
    PartyId PractitionerId { get; }
    DateOnly Date { get; }
}