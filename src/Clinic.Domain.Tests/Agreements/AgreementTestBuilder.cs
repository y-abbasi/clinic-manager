using Clinic.Domain.Agreements;
using Clinic.Domain.Contracts.Agreements;
using Clinic.Domain.Contracts.Parties;
using Clinic.Domain.Tests.Parties.Organizations;
using Clinic.Domain.Tests.Parties.People;
using Core.SharedKernels;

namespace Clinic.Domain.Tests.Agreements;

internal class AgreementTestBuilder : IAgreementOptions
{
    public readonly AgreementManager Manager = new();

    public PartyId OrganizationId => Manager.OrganizationId;
    public PartyId PractitionerId => Manager.PractitionerId;
    public Range<DateOnly> AgreementPeriod => Manager.AgreementPeriod;
    public List<ScheduleOption> Schedules { get; } = new();
    IEnumerable<IScheduleOption> IAgreementOptions.Schedules => Manager.Schedules;

    public AgreementTestBuilder()
    {
        Manager
            .WithOrganization(new OrganizationTestBuilder().WithHealthCareRole().Build())
            .WithPractitioner(new PersonTestBuilder().IsDoctor().Build())
            .WithSchedules([
                new ScheduleOption(DayOfWeek.Friday,
                    [new Range<TimeOnly>(new TimeOnly(9, 0), new TimeOnly(10, 0))])
            ])
            .WithAgreementPeriod(TestConstants.ValidAgreementPeriod);
    }

    public IAgreement Build() => Manager.Build();

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
        Manager.WithOrganization(builder.Build());
        return this;
    }

    public AgreementTestBuilder WithPractitioner(Func<PersonTestBuilder, PersonTestBuilder>? configure)
    {
        var builder = new PersonTestBuilder();
        builder = configure?.Invoke(builder) ?? builder;
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

    public AgreementTestBuilder WithSchedulesThatConflictWithHealthCareWorkingSchedule()
    {
        Manager.WithSchedules([
            new ScheduleOption(DayOfWeek.Thursday, [
                new Range<TimeOnly>(new TimeOnly(8), new TimeOnly(18))
            ])
        ]);
        return this;
    }
}