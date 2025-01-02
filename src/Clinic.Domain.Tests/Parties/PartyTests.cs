using Clinic.Domain.Contracts.Parties;
using Clinic.Domain.Contracts.Parties.People;

namespace Clinic.Domain.Tests.Parties;

public abstract class PartyTests<TBuilder, TAgg>
    where TBuilder : IPartyTestBuilder<TBuilder, TAgg>
    where TAgg : IParty
{
    protected abstract TBuilder CreateSutBuilder();

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