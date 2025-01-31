using Clinic.Domain.Agreements;
using Clinic.Domain.Contracts.Agreements;
using Clinic.Domain.Contracts.Parties;
using Clinic.Domain.Contracts.Sessions;
using Clinic.Domain.Parties.Organizations;
using Clinic.Domain.Parties.People;
using Clinic.Domain.Tests.Parties.Organizations;
using Clinic.Domain.Tests.Parties.People;
using Core.SharedKernels;
using NSubstitute;
using NSubstitute.Extensions;

namespace Clinic.Domain.Tests.Agreements;

internal class AgreementTestBuilder : IAgreementOptions
{
    public readonly AgreementManager Manager = new();
    private readonly ISessionService _sessionService = Substitute.For<ISessionService>();

    public PartyId OrganizationId => Manager.OrganizationId;
    public PartyId PractitionerId => Manager.PractitionerId;
    public Range<DateOnly> AgreementPeriod => Manager.AgreementPeriod;
    public List<ScheduleOption> Schedules { get; } = new();
    IEnumerable<IScheduleOption> IAgreementOptions.Schedules => Manager.Schedules;

    public AgreementTestBuilder()
    {
        WithOrganization(b => b.WithHealthCareRole(
                builder => builder.WithWorkingSchedulesAtMondayAndWednesdayAt8_00To_20_00()))
            .WithSchedules([
                TestConstants.ScheduleAtMondayFrom8_00To20_00
            ])
            .WithPractitioner(builder => builder.IsDoctor());
        Manager
            .WithAgreementPeriod(TestConstants.ValidAgreementPeriod);
    }

    public IAgreement Build() => Manager.Build();

    public Organization Organization { get; set; }

    public Person Practitioner { get; set; }

    public AgreementTestBuilder WithoutOrganization()
    {
        Manager.WithOrganization(null);
        return this;
    }

    public AgreementTestBuilder WithoutPractitioner()
    {
        Manager.WithPractitioner(null);
        return this;
    }

    public AgreementTestBuilder WithOrganization(Func<OrganizationTestBuilder, OrganizationTestBuilder>? configure)
    {
        var builder = new OrganizationTestBuilder();
        builder = configure?.Invoke(builder) ?? builder;
        Organization = builder.Build();
        Manager.WithOrganization(Organization);
        return this;
    }

    public AgreementTestBuilder WithPractitioner(Func<PersonTestBuilder, PersonTestBuilder>? configure)
    {
        var builder = new PersonTestBuilder();
        builder = configure?.Invoke(builder) ?? builder;
        Practitioner = builder.Build();
        Manager.WithPractitioner(builder.Build());
        return this;
    }

    public AgreementTestBuilder WithoutAnySchedule()
    {
        Manager.WithSchedules([]);
        return this;
    }

    public AgreementTestBuilder WithSchedules(List<ScheduleOption> schedules)
    {
        Manager.WithSchedules(schedules);
        return this;
    }

    public AgreementTestBuilder WithSchedulesThatConflictWithHealthCaresWorkingSchedule()
    {
        Manager
            .WithOrganization(new OrganizationTestBuilder()
                .WithHealthCareRole(b => b.WithWorkingSchedules([
                    new Schedule(DayOfWeek.Monday, [
                        new Range<TimeOnly>(new TimeOnly(8, 0), new TimeOnly(18, 0))
                    ]),
                    new Schedule(DayOfWeek.Friday, [
                        new Range<TimeOnly>(new TimeOnly(8, 0), new TimeOnly(18, 0))
                    ])
                ])).Build()).WithSchedules([
                new ScheduleOption(DayOfWeek.Thursday, [
                    new Range<TimeOnly>(new TimeOnly(8), new TimeOnly(18))
                ])
            ]);
        return this;
    }

    public Task<ISession> GetOrCreateSession(DateTime date)
    {
        return Build().GetOrCreateSessionAsync(_sessionService, date);
    }

    public ISession ThereIsASessionFor(DateOnly date)
    {
        var session = Substitute.For<ISession>();
        _sessionService.GetAsync(new SessionId(OrganizationId, PractitionerId, date))
            .Returns(Task.FromResult(session));

        return session;
    }

    public AgreementTestBuilder ThereIsNotAnySessionFor(DateTime date)
    {
        _sessionService.GetAsync(Arg.Is<SessionId>(s =>
                s.OrganizationId == OrganizationId && s.PractitionerId == PractitionerId &&
                s.Date == DateOnly.FromDateTime(date)))
            .Returns(Task.FromResult<ISession?>(null));

        return this;
    }
}