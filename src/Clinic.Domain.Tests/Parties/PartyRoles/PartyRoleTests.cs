using Clinic.Domain.Contracts.Parties.PartyRoles;

namespace Clinic.Domain.Tests.Parties.PartyRoles;

public abstract class PartyRoleTests<TBuilder, TEntity>
    where TBuilder : class, IPartyRoleTestBuilder<TBuilder, TEntity>
    where TEntity : IPartyRole
{
    public PartyRoleTests()
    {
        SutBuilder = CreateSutBuilder();
    }

    protected abstract IPartyRoleTestBuilder<TBuilder, TEntity> CreateSutBuilder();
    private IPartyRoleTestBuilder<TBuilder, TEntity> SutBuilder;

    [Fact]
    public void PartyRoleCreatedSuccessfully()
    {
        //arrange

        //act
        var sut = SutBuilder.Build();

        //assert
        sut.Should().BeEquivalentTo(SutBuilder);
    }
}