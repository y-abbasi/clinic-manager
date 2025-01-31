using Clinic.Domain.Contracts.Parties;
using Clinic.Domain.Contracts.Parties.Organizations;
using Clinic.Domain.Parties.PartyRoles.Managers;

namespace Clinic.Domain.Parties.Organizations;

public class Organization : Party, IOrganization
{
    public string Name { get; private set; } = default!;
    public virtual PartyType PartyType => PartyType.Organization;

    private Organization()
    {
    }

    public Organization(IOrganizationOptions options, PartyRoleManager partyRoleManager)
        : base(options, partyRoleManager)
    {
        updateProperties(options);
    }

    private void updateProperties(IOrganizationOptions options)
    {
        Name = options.Name;
    }
}