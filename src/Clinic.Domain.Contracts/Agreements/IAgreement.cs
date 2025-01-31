using Clinic.Domain.Contracts.Sessions;
using Core.Domain;

namespace Clinic.Domain.Contracts.Agreements;

public interface IAgreement : IAggregateRoot<AgreementId>, IAgreementOptions
{
    Task<ISession> GetOrCreateSessionAsync(ISessionService sessionService, DateTime date);
}