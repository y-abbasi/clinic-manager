using Clinic.Domain.Agreements.Exceptions;
using Clinic.Domain.Contracts.Agreements;
using Clinic.Domain.Contracts.Sessions;
using Core.Domain;
using Core.SharedKernels;
using NSubstitute;
using Xunit.Abstractions;

namespace Clinic.Domain.Tests.Agreements;

public class AgreementTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    AgreementTestBuilder SutBuilder = new();

    public AgreementTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    #region Happy Path

    [Fact]
    public void Constructor_Should_CreatesAgreement_Properly()
    {
        //arrange

        //act
        var sut = SutBuilder.Build();

        //assert
        sut.Should().BeEquivalentTo<IAgreementOptions>(SutBuilder);
        sut.Id.Value.Should().NotBe(Guid.Empty);
    }

    [Theory]
    [InlineData(9, 10, 11, 12)] //( <) >  
    [InlineData(13, 16, 9, 10)] //(< ) > 
    public void Constructor_Should_CreatesAgreement_InBoundary_Properly(
        int shift1StartHour, int shift1EndHour, int shift2StartHour, int shift2EndHour)
    {
        //arrange
        SutBuilder.WithoutAnySchedule()
            .WithSchedules([
                new(DayOfWeek.Monday,
                [
                    new Range<TimeOnly>(new TimeOnly(shift1StartHour, 0), new TimeOnly(shift1EndHour, 0)),
                    new Range<TimeOnly>(new TimeOnly(shift2StartHour, 0), new TimeOnly(shift2EndHour, 0))
                ]),
            ]);

        //act
        var sut = SutBuilder.Build();

        //assert
        sut.Should().BeEquivalentTo<IAgreementOptions>(SutBuilder);
    }

    [Fact]
    public void Constructor_Should_Merge_Schedules_Of_Same_Day_Properly()
    {
        //arrange
        SutBuilder.WithoutAnySchedule()
            .WithSchedules([
                new(DayOfWeek.Monday,
                [
                    new Range<TimeOnly>(new TimeOnly(8, 0), new TimeOnly(12, 0)),
                ]),
                new(DayOfWeek.Monday,
                [
                    new Range<TimeOnly>(new TimeOnly(13, 0), new TimeOnly(18, 0))
                ]),
            ]);

        //act
        var sut = SutBuilder.Build();

        //assert
        sut.Should().BeEquivalentTo<IAgreementOptions>(ExpectedBuilder());

        AgreementTestBuilder ExpectedBuilder() =>
            SutBuilder.WithoutAnySchedule()
                .WithSchedules([
                    new(DayOfWeek.Monday,
                    [
                        new Range<TimeOnly>(new TimeOnly(8, 0), new TimeOnly(12, 0)),
                        new Range<TimeOnly>(new TimeOnly(13, 0), new TimeOnly(18, 0))
                    ]),
                ]);
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
    [InlineData(9, 10, 9, 11)] //(< ) > 
    [InlineData(8, 10, 9, 11)] //( < ) > 
    [InlineData(9, 11, 8, 9)] //( < > )
    [InlineData(9, 11, 9, 10)] //<( > ) 
    [InlineData(9, 11, 8, 10)] //< ( > ) 
    [InlineData(9, 11, 8, 11)] //< ( >) 
    [InlineData(9, 11, 8, 12)] //< ( ) > 
    [InlineData(9, 11, 9, 11)] //<( )> 
    public void Constructor_Should_Throw_DomainException_When_Overlap_Exists_On_Schedules(
        int shift1StartHour, int shift1EndHour, int shift2StartHour, int shift2EndHour)
    {
        //arrange
        SutBuilder.WithoutAnySchedule()
            .WithSchedules([
                new(DayOfWeek.Monday,
                [
                    new Range<TimeOnly>(new TimeOnly(shift1StartHour, 0), new TimeOnly(shift1EndHour, 0)),
                    new Range<TimeOnly>(new TimeOnly(shift2StartHour, 0), new TimeOnly(shift2EndHour, 0))
                ]),
            ]);

        //act
        var act = () => SutBuilder.Build();

        //assert
        act.Should().Throw<DomainException>().Which.Should()
            .BeEquivalentTo(new { Code = "AGR-06", Message = "Overlapping schedules are not supported." });
    }

    [Fact]
    public void Constructor_Should_Throw_DomainException_When_Schedule_Has_Conflict_With_Health_Care_WorkingSchedule()
    {
        //arrange
        SutBuilder
            .WithoutAnySchedule()
            .WithSchedulesThatConflictWithHealthCaresWorkingSchedule();
        //act
        var act = () => SutBuilder.Build();

        //assert
        act.Should().Throw<DomainException>().Which.Should()
            .BeEquivalentTo(new { Code = "AGR-07", Message = "Schedule should be in health care working times." });
    }

    #endregion

    #region Happy Path for GetOrCreateSession

    [Fact]
    public async Task GetOrCreateSession_CreatesNewSession_IfNoneExists()
    {
        //arrange
        var date = TestConstants.SomeDateTimeAtMonday_9h_30mAm;
        SutBuilder.ThereIsNotAnySessionFor(date);

        //act
        var session = await SutBuilder.GetOrCreateSession(date);

        //assert
        session.Id.Should().BeEquivalentTo(new SessionId(SutBuilder.OrganizationId,
            SutBuilder.PractitionerId,
            DateOnly.FromDateTime(date)));
    }

    [Fact]
    public async Task GetOrCreateSession_Returns_ExistingSession_If_AlreadyPresent()
    {
        //arrange
        var date = TestConstants.SomeDateTimeAtMonday_9h_30mAm;
        var expected = SutBuilder.ThereIsASessionFor(DateOnly.FromDateTime(date));

        //act
        var session = await SutBuilder.GetOrCreateSession(date);

        //assert
        session.Should().Be(expected);
    }

    #endregion

    #region Exceptional flow for GetOrCreateSessions

    [Fact]
    public async Task GetOrCreateSession_Throws_Exception_If_SessionDateOutOfAgreementSchedule()
    {
        //arrange
        var date = TestConstants.SomeDateTimeAtWednesday_9h_30mAm; //at sunday
        SutBuilder
            .WithOrganization(b => b.WithHealthCareRole(
                builder => builder.WithWorkingSchedulesAtMondayAndWednesdayAt8_00To_20_00()))
            .WithSchedules([
                TestConstants.ScheduleAtMondayFrom8_00To20_00
            ])
            .ThereIsNotAnySessionFor(date);
        //act
        var action = () => SutBuilder.GetOrCreateSession(date);

        //assert
        await action.Should().ThrowAsync<OrganizationOrPractitionerNotAvailableAtTheRequestedDate>();
    }
    
    #endregion
}