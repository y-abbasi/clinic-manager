using System.Reflection;
using Clinic.Domain.Parties.PartyRoles;
using Clinic.Domain.Parties.PartyRoles.HealthCares;
using Core.Domain;

namespace Clinic.Domain.Tests.Parties.PartyRoles.HealthCares;

public class HealthCareTests : PartyRoleTests<HealthCareTestBuilder, HealthCare>
{
    public HealthCareTests()
    {
        SutBuilder = CreateSutBuilder();
    }

    protected override HealthCareTestBuilder CreateSutBuilder() => new();
    private HealthCareTestBuilder SutBuilder;

    [Fact]
    public void Constructor_CreatesHealthCare()
    {
        var sut = SutBuilder.Build();
        sut.Should().BeEquivalentTo(SutBuilder);
        
    }
  
    #region Exceptional flow for Title

    [Fact]
    public void Constructor_Should_Throw_Exception_If_Title_Is_Null()
    {
        //arrange
        SutBuilder.WithTitle(null);

        //act
        var act = () => SutBuilder.Build();

        //assert
        act.Should().Throw<DomainException>();
    }


    [Fact]
    public void Constructor_Should_Throw_Exception_If_Schedule_Does_Not_Exist()
    {
        //arrange
        SutBuilder.WithWorkingSchedules([]);

        //act
        var act = () => SutBuilder.Build();

        //assert
        act.Should().Throw<DomainException>()
            .Which.Should()
            .BeEquivalentTo(new {Code="HLC-02", Message="At least one working schedule is required." });
    }

    #endregion
}