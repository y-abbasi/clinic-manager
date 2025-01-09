namespace Clinic.Domain.Contracts.Parties.PartyRoles;

public interface IPartyRole : IPartyRoleOptions
{
    bool ApplicableToParty(IParty party);
}