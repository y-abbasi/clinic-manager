using Clinic.Domain.Contracts.Parties;
using Core.Domain;

namespace Clinic.Domain.Contracts.Sessions;

public interface ISession : IAggregateRoot<SessionId>
{
}

public record SessionId(PartyId OrganizationId, PartyId PractitionerId, DateOnly Date);

public interface ISessionOption
{
    PartyId OrganizationId { get; }
    PartyId PractitionerId { get; }
    DateOnly Date { get; }
}