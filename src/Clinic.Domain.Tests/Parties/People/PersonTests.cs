using Clinic.Domain.Contracts.Parties.People;
using Clinic.Domain.Parties.People;
using FluentAssertions;

namespace Clinic.Domain.Tests.Parties.People;

public class PersonTests : PartyTests<PersonTestBuilder, Person>
{
    protected override PersonTestBuilder CreateSutBuilder()
    {
        return new PersonTestBuilder()
            .IsDoctor();
    }

    [Fact]
    public void PersonCanNotBeAHealthCare()
    {
        //arrange
        var sutBuilder = CreateSutBuilder().WithHealthCareRole();
        
        //act
        var action = () => sutBuilder.Build();
        
        //assert
        action.Should().Throw<Exception>();
    }
}