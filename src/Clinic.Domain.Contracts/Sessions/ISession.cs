using Clinic.Domain.Contracts.Parties;
using Clinic.Domain.Contracts.Parties.People;
using Core.Domain;

namespace Clinic.Domain.Contracts.Sessions;

public interface ISession : IAggregateRoot<SessionId>
{
    IEnumerable<IAppointment> Appointments { get; }
}

public interface IAppointmentOption
{
    DateTime Time { get; }
    PartyId Patient { get; }
    int DurationMinute { get; }
}
public interface IAppointment : IEntity<AppointmentId>, IAppointmentOption
{
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