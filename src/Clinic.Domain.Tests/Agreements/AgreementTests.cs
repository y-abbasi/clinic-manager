using Clinic.Domain.Contracts.Agreements;
using Core.Domain;

namespace Clinic.Domain.Tests.Agreements;

public class AgreementTests
{
    AgreementTestBuilder SutBuilder = new();

    [Fact]
    public void Constructor_Should_CreatesAgreement_Properly()
    {
        //arrange

        //act
        var sut = SutBuilder.Build();

        //assert
        sut.Should().BeEquivalentTo<IAgreementOptions>(SutBuilder);
    }

    #region Exceptional flow for Organization

    [Fact]
    public void Constructor_Should_Throw_DomainException_When_OrganizationIsNull()
    {
        //arrange
        SutBuilder.WithoutOrganization();
        
        //act
        var act = () => SutBuilder.Build();
        
        //assert
        act.Should().Throw<DomainException>();
    }
    [Fact]
    public void Constructor_Should_Throw_DomainException_When_OrganizationDoesNotHave_HealthCare_Role()
    {
        //arrange
        SutBuilder.WithOrganization(builder => builder.WithoutAnyRole());
        
        //act
        var act = () => SutBuilder.Build();
        
        //assert
        act.Should().Throw<DomainException>();
    }

    #endregion
}