using Clinic.Domain.Contracts.Sessions;
using Core.Domain;

namespace Clinic.Domain.Sessions;

public class Session : AggregateRoot<SessionId>, ISession
{
    internal Session(ISessionOption option)
    {
        Id = new SessionId(option.OrganizationId, option.PractitionerId, option.Date);
    }
}