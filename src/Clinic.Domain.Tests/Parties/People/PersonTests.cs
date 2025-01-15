using Clinic.Domain.Contracts.Parties.PartyRoles;
using Clinic.Domain.Contracts.Parties.People;
using Clinic.Domain.Parties.Exceptions;
using Clinic.Domain.Parties.People;
using Clinic.Domain.Tests.Parties.PartyRoles.Doctors;
using Clinic.Domain.Tests.Parties.PartyRoles.HealthCares;
using FluentAssertions;

namespace Clinic.Domain.Tests.Parties.People;

public class PersonTests : PartyTests<PersonTestBuilder, Person>
{
    protected override PersonTestBuilder CreateSutBuilder()
    {
        return new PersonTestBuilder()
            .IsDoctor();
    }

    protected override IPartyRoleOptions[] AcceptableRoles => [new DoctorTestBuilder().BuildOptions()]; 
    protected override IPartyRoleOptions[] UnAcceptableRoles =>[new HealthCareTestBuilder().BuildOptions()];

    [Fact]
    public void Constructor_Should_Throw_Exception_When_FirstName_Is_Null()
    {
        //arrange
        var sutBuilder = CreateSutBuilder()
            .WithFirstName(null);
        //act
        var action = () => sutBuilder.Build();
        
        //assert
        action.Should().Throw<FirstNameRequired>();
        
    }
}

