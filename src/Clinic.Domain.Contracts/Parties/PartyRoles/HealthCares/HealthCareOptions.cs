namespace Clinic.Domain.Contracts.Parties.PartyRoles.HealthCares;

public class HealthCareOptions : IPartyRoleOptions
{
    public string Code { get; set; } = default!;
    public string Title { get; set; } = default!;
}