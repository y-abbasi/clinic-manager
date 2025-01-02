using Clinic.Domain.Contracts.Parties.People;
using Clinic.Domain.Parties.People;

namespace Clinic.Domain.Tests.Parties.People;

internal class PersonTestBuilder : IPersonOptions
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public PersonTestBuilder()
    {
        FirstName = TestConstants.SomeName;
        LastName = TestConstants.SomeLastName;
    }
    public Person Build()
    {
        return new(this);
    }
}