using Clinic.Domain.Contracts.Parties.PartyRoles;
using Clinic.Domain.Contracts.Parties.PartyRoles.Doctors;
using Clinic.Domain.Contracts.Parties.People;
using Clinic.Domain.Parties.People;
using Clinic.Domain.Tests.Parties.PartyRoles;
using Clinic.Domain.Tests.Parties.PartyRoles.Doctors;

namespace Clinic.Domain.Tests.Parties.People;

public class PersonTestBuilder : PartyTestBuilder, IPersonOptions
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<IPartyRoleOptions> PartyRoles { get; } = new();

    public PersonTestBuilder()
    {
        FirstName = TestConstants.SomeName;
        LastName = TestConstants.SomeLastName;
    }
    public override Person Build()
    {
        return new(this);
    }

    public PersonTestBuilder IsDoctor()
    {
        // PartyRoles.Add(new DoctorTestBuilder());
        return this;
    }
}