using Clinic.Domain.Contracts.Parties;
using Clinic.Domain.Contracts.Parties.PartyRoles;
using Clinic.Domain.Contracts.Parties.PartyRoles.Doctors;
using Clinic.Domain.Contracts.Parties.People;
using Clinic.Domain.Parties.PartyRoles.Managers;
using Clinic.Domain.Parties.People;
using Clinic.Domain.Tests.Parties.PartyRoles;
using Clinic.Domain.Tests.Parties.PartyRoles.Doctors;
using Newtonsoft.Json;

namespace Clinic.Domain.Tests.Parties.People;

public class PersonTestBuilder : PartyTestBuilder, IPersonOptions
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<IPartyRoleOptions> PartyRoles { get; } = new();
    IEnumerable<IPartyRoleOptions> IPartyOptions.PartyRoles => PartyRoles;

    public PersonTestBuilder()
    {
        FirstName = TestConstants.SomeName;
        LastName = TestConstants.SomeLastName;
    }
    public override Person Build()
    {
        return new(this, new PartyRoleManager());
    }

    public PersonTestBuilder IsDoctor()
    {
         PartyRoles.Add(new DoctorTestBuilder().BuildOptions());

        return this;
    }
}