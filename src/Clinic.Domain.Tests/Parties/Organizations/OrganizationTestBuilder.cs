using Clinic.Domain.Contracts.Parties.Organizations;
using Clinic.Domain.Contracts.Parties.People;
using Clinic.Domain.Parties.Organizations;
using Clinic.Domain.Parties.People;

namespace Clinic.Domain.Tests.Parties.Organizations;

public class OrganizationTestBuilder : PartyTestBuilder, IOrganizationOptions
{
    public string Name { get; set; }

    public OrganizationTestBuilder()
    {
        Name = TestConstants.SomeName;
    }
    public override Organization Build()
    {
        return new(this);
    }
}