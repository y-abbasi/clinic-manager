using Clinic.Domain.Contracts.Parties.People;
using Clinic.Domain.Parties.PartyRoles.Managers;
using Clinic.Domain.Parties.People;

namespace Clinic.Domain.Tests.Parties.People;

public class PersonTestBuilder<TSelf> : PartyTestBuilder<TSelf, Person>, IPersonOptions 
    where TSelf : class, IPartyTestBuilder<TSelf, Person>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public PersonTestBuilder()
    {
        FirstName = TestConstants.SomeName;
        LastName = TestConstants.SomeLastName;
    }
    public TSelf WithFirstName(string firstName)
    {
        FirstName = firstName;
        return this;
    }
    public override Person Build()
    {
        return new(this, new PartyRoleManager());
    }
}

public class PersonTestBuilder : PersonTestBuilder<PersonTestBuilder>, IPartyTestBuilder<PersonTestBuilder,Person>
{

}