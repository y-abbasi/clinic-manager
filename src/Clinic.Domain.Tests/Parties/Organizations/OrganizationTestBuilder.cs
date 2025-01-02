using Clinic.Domain.Contracts.Parties.Organizations;
using Clinic.Domain.Contracts.Parties.People;
using Clinic.Domain.Parties.Organizations;
using Clinic.Domain.Parties.PartyRoles.Managers;
using Clinic.Domain.Parties.People;

namespace Clinic.Domain.Tests.Parties.Organizations;

public class OrganizationTestBuilder <TSelf> : PartyTestBuilder<TSelf, Organization>, IOrganizationOptions 
where TSelf : class, IPartyTestBuilder<TSelf, Organization>
{
    public string Name { get; set; }

    public OrganizationTestBuilder()
    {
        Name = TestConstants.SomeName;
    }
    public override Organization Build()
    {
        return new(this, new PartyRoleManager());
    }


}

public class OrganizationTestBuilder : OrganizationTestBuilder<OrganizationTestBuilder>
{
}