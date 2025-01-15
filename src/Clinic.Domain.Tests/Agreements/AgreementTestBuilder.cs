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
    public List<Schedule> Schedules { get; } = new();
    IEnumerable<Schedule> IAgreementOptions.Schedules => Manager.Schedules;

    public AgreementTestBuilder()
    {
        Manager
            .WithOrganization(new OrganizationTestBuilder().WithHealthCareRole().Build())
            .WithPractitioner(new PersonTestBuilder().IsDoctor().Build())
            .AddSchedule(new Schedule(DayOfWeek.Monday, new Range<TimeOnly>(new TimeOnly(8), new TimeOnly(12))))
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
}