using Clinic.Domain.Contracts.Parties.Organizations;
using Clinic.Domain.Contracts.Parties.People;
using Clinic.Domain.Parties.Organizations;

namespace Clinic.Domain.Tests.Parties.Organizations;

public class OrganizationTests : PartyTests<OrganizationTestBuilder, Organization>
{
    protected override OrganizationTestBuilder CreateSutBuilder()
    {
        return new OrganizationTestBuilder()
            .WithHealthCareRole();
    }
}