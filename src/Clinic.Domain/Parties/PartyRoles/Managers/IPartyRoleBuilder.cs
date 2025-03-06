namespace Clinic.Domain.Parties.PartyRoles.Managers;

public interface IPartyRoleBuilder
{
    string Code { get; }
    Type GetPartyRoleType();
    Type GetPartyRoleOptionType();
}