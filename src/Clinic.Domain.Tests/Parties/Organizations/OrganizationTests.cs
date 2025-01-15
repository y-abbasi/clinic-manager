using Clinic.Domain.Contracts.Parties.Organizations;
using Clinic.Domain.Contracts.Parties.PartyRoles;
using Clinic.Domain.Contracts.Parties.People;
using Clinic.Domain.Parties.Organizations;
using Clinic.Domain.Tests.Parties.PartyRoles.Doctors;
using Clinic.Domain.Tests.Parties.PartyRoles.HealthCares;

namespace Clinic.Domain.Tests.Parties.Organizations;

public class OrganizationTests : PartyTests<OrganizationTestBuilder, Organization>
{
    protected override IPartyRoleOptions[] AcceptableRoles =>[new HealthCareTestBuilder().BuildOptions()];
    protected override IPartyRoleOptions[] UnAcceptableRoles => [new DoctorTestBuilder().BuildOptions()]; 


    protected override OrganizationTestBuilder CreateSutBuilder()
    {
        return new OrganizationTestBuilder()
            .WithHealthCareRole();
    }
}