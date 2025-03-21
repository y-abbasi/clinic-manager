using Clinic.Domain.Contracts.Parties;
using Clinic.Domain.Contracts.Sessions;
using Core.Domain;

namespace Clinic.Domain.Contracts.Agreements;

public interface IAgreement : IAggregateRoot<AgreementId>, IAgreementOptions
{
    Task<ISession> GetOrCreateSessionAsync(ISessionService sessionService, DateTime date);
    Task<ISession> SetAppointmentAsync(IAppointmentOption option, ISessionService sessionService, IPartyService partyService, IClientAppointmentService clientAppointmentService);
}