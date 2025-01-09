using Core.Domain;

namespace Clinic.Domain.Contracts.Parties;

public interface IParty : IAggregateRoot<PartyId>, IPartyOptions
{
}

public enum PartyType
{
    Person, Organization
}