namespace Clinic.Domain.Contracts.Parties;

public interface IPartyService
{
    Task<IParty> GetParty(PartyId id);
}