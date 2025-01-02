using Clinic.Domain.Contracts.Parties.People;
using FluentAssertions;

namespace Clinic.Domain.Tests.Parties.People;

public class PersonTests : PartyTests<PersonTestBuilder, IPersonOptions>
{
    protected override PersonTestBuilder CreateSutBuilder()
    {
        return new PersonTestBuilder();
    }

    public void APersonCanBeADoctor()
    {
        //arrange
        var builder = CreateSutBuilder()
            .IsDoctor();
        
        //act
    }
}