using Clinic.Domain.Contracts.Parties;

namespace Clinic.Domain.Contracts.Agreements;

public interface IAgreementService
{
    Task<IAgreement> GetAsync(PartyId organisationId, PartyId practitionerId, DateTime activatedAt);
}