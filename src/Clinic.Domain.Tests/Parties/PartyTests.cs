using Clinic.Domain.Contracts.Parties.People;

namespace Clinic.Domain.Tests.Parties;

public abstract class PartyTests<TBuilder, TOption>
    where TBuilder : IPartyTestBuilder, TOption
    where TOption : IPartyOptions
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
        sut.Should().BeEquivalentTo<TBuilder>(sutBuilder);
        sut.Id.Should().NotBeNull();
    }
}