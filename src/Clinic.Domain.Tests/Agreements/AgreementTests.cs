using Clinic.Domain.Contracts.Agreements;
using Core.Domain;
using Core.SharedKernels;

namespace Clinic.Domain.Tests.Agreements;

public class AgreementTests
{
    AgreementTestBuilder SutBuilder = new();

    #region Happy Path

    [Fact]
    public void Constructor_Should_CreatesAgreement_Properly()
    {
        //arrange

        //act
        var sut = SutBuilder.Build();

        //assert
        sut.Should().BeEquivalentTo<IAgreementOptions>(SutBuilder);
    }
    
    [Theory]
    [InlineData(9, 10, 11, 12)] //( <) >  
    [InlineData(13, 16, 9, 10)]  //(< ) > 
    public void Constructor_Should_CreatesAgreement_InBoundary_Properly(
        int shift1StartHour, int shift1EndHour, int shift2StartHour, int shift2EndHour)
    {
        //arrange
        SutBuilder.WithoutAnySchedule()
            .WithSchedules([
                new(DayOfWeek.Monday,
                    new Range<TimeOnly>(new TimeOnly(shift1StartHour), new TimeOnly(shift1EndHour))),
                new(DayOfWeek.Monday, new Range<TimeOnly>(new TimeOnly(shift2StartHour), new TimeOnly(shift2EndHour))),
            ]);

        //act
        var sut = SutBuilder.Build();

        //assert
        sut.Should().BeEquivalentTo<IAgreementOptions>(SutBuilder);
    }
    
    #endregion
    
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

    #region Exceptional flow for Practitioner

    [Fact]
    public void Constructor_Should_Throw_DomainException_When_PractitionerIsNull()
    {
        //arrange
        SutBuilder.WithoutPractitioner();

        //act
        var act = () => SutBuilder.Build();

        //assert
        act.Should().Throw<DomainException>();
    }

    [Fact]
    public void Constructor_Should_Throw_DomainException_When_PractitionerDoesNotHave_Doctor_Role()
    {
        //arrange
        SutBuilder.WithPractitioner(builder => builder.WithoutAnyRole());

        //act
        var act = () => SutBuilder.Build();

        //assert
        act.Should().Throw<DomainException>();
    }

    #endregion

    #region Exceptional flow for Schedule

    [Fact]
    public void Constructor_Should_Throw_DomainException_When_ScheduleIsNotDefined()
    {
        //arrange
        SutBuilder.WithoutAnySchedule();

        //act
        var act = () => SutBuilder.Build();

        //assert
        act.Should().Throw<DomainException>().Which.Should()
            .BeEquivalentTo(new { Code = "AGR-05", Message = "At least one schedule is required." });
    }

    [Theory]
    [InlineData(9, 10, 10, 11)] //( <) >  
    [InlineData(9, 10, 9, 11)]  //(< ) > 
    [InlineData(8, 10, 9, 11)]  //( < ) > 
    [InlineData(9, 11, 8, 9)]   //( < > )
    [InlineData(9, 11, 9, 10)]  //< ( > ) 
    [InlineData(9, 11, 8, 10)]  //< ( > ) 
    [InlineData(9, 11, 8, 11)]  //< ( >) 
    [InlineData(9, 11, 8, 12)]  //< ( ) > 
    [InlineData(9, 11, 9, 11)]  //<( )> 
    public void Constructor_Should_Throw_DomainException_When_Overlap_Exists_On_Schedules(
        int shift1StartHour, int shift1EndHour, int shift2StartHour, int shift2EndHour)
    {
        //arrange
        SutBuilder.WithoutAnySchedule()
            .WithSchedules([
                new(DayOfWeek.Monday,
                    new Range<TimeOnly>(new TimeOnly(shift1StartHour), new TimeOnly(shift1EndHour))),
                new(DayOfWeek.Monday, new Range<TimeOnly>(new TimeOnly(shift2StartHour), new TimeOnly(shift2EndHour))),
            ]);

        //act
        var act = () => SutBuilder.Build();

        //assert
        act.Should().Throw<DomainException>().Which.Should()
            .BeEquivalentTo(new { Code = "AGR-06", Message = "Overlapping schedules are not supported." });
    }

    #endregion
}