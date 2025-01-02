using Clinic.Domain.Contracts.Parties;
using Clinic.Domain.Contracts.Parties.Organizations;

namespace Clinic.Domain.Parties.Organizations;

public class Organization : Party, IOrganization
{
    public string Name { get; private set; } = default!;

    private Organization()
    {
    }

    public Organization(IOrganizationOptions options)
    {
        Id = PartyId.New();
        updateProperties(options);
    }

    private void updateProperties(IOrganizationOptions options)
    {
        Name = options.Name;
    }
}