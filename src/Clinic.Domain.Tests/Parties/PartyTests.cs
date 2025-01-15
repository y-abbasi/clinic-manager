using Clinic.Domain.Contracts.Parties;
using Clinic.Domain.Contracts.Parties.PartyRoles;
using Clinic.Domain.Contracts.Parties.People;
using Clinic.Domain.Parties.Exceptions;

namespace Clinic.Domain.Tests.Parties;

public abstract class PartyTests<TBuilder, TAgg>
    where TBuilder : IPartyTestBuilder<TBuilder, TAgg>
    where TAgg : IParty
{
    protected abstract TBuilder CreateSutBuilder();
    protected abstract IPartyRoleOptions[] AcceptableRoles { get; }
    protected abstract IPartyRoleOptions[] UnAcceptableRoles { get; }

    [Fact]
    public void Constructor_Should_Create_Person_Properly()
    {
        //arrange
        var sutBuilder = CreateSutBuilder();

        //act
        var sut = sutBuilder.Build();

        //assert
        sut.Should().BeEquivalentTo(sutBuilder);
        sut.Id.Should().NotBeNull();
    }

    [Fact]
    public void Constructor_Should_Create_Person_With_Appropriate_Role_Properly()
    {
        //arrange
        var sutBuilder = CreateSutBuilder()
            .WithPartyRoles(AcceptableRoles);

        //act
        var sut = sutBuilder.Build();

        //assert
        sut.Should().BeEquivalentTo(sutBuilder);
        sut.Id.Should().NotBeNull();
    }

    [Fact]
    public void Constructor_Should_Throw_Exception_If_Party_Roles_Is_Unacceptable()
    {
        //arrange
        var sutBuilder = CreateSutBuilder()
            .WithPartyRoles(UnAcceptableRoles);

        //act
        var action = ()=> sutBuilder.Build();

        //assert
        action.Should().Throw<PartyRoleCanNotAssignableToParty>();
    }
}