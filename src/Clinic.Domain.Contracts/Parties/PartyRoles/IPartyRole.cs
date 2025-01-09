namespace Clinic.Domain.Contracts.Parties.PartyRoles;

public interface IPartyRole : IPartyRoleOptions
{
    bool AcceptedByPartyType(IParty party);
}