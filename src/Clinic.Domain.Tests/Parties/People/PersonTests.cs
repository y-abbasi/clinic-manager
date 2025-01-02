using FluentAssertions;

namespace Clinic.Domain.Tests.Parties.People;

public class PersonTests
{
    private PersonTestBuilder CreateSutBuilder()
    {
        return new();
    }

    [Fact]
    public void PersonCreatedSuccessfully()
    {
        //arrange
        var sutBuilder = CreateSutBuilder();
        
        //act
        var sut = sutBuilder.Build();
        
        //assert
        sut.Should().BeEquivalentTo(sutBuilder);
        sut.Id.Should().NotBeNull();
    }
}

