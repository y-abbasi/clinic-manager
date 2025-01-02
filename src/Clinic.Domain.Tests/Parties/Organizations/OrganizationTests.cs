using Clinic.Domain.Contracts.Parties.Organizations;
using Clinic.Domain.Contracts.Parties.People;

namespace Clinic.Domain.Tests.Parties.Organizations;

public class OrganizationTests : PartyTests<OrganizationTestBuilder, IOrganizationOptions>
{
    protected override OrganizationTestBuilder CreateSutBuilder()
    {
        return new OrganizationTestBuilder();
    }
}