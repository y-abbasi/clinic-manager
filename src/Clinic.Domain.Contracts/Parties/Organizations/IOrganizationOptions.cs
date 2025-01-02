namespace Clinic.Domain.Contracts.Parties.Organizations;

public interface IOrganizationOptions : IPartyOptions
{
    string Name { get; }
}