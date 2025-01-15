using System.Collections.Immutable;
using Clinic.Domain.Contracts.Agreements;
using Clinic.Domain.Contracts.Parties;
using Clinic.Domain.Contracts.Parties.Organizations;
using Clinic.Domain.Contracts.Parties.People;
using Core.Domain;
using Core.SharedKernels;

namespace Clinic.Domain.Agreements;

public class Agreement : AggregateRoot<AgreementId>, IAgreement
{
    public Agreement(IAgreementCreatorOptions  options)
    {
        SetupProperties(options);
    }

    private void SetupProperties(IAgreementCreatorOptions options)
    {
        OrganizationId = options.OrganizationId;
        PractitionerId = options.PractitionerId;
        AgreementPeriod = options.AgreementPeriod;
        Schedules = options.Schedules.ToImmutableList();
    }

    public PartyId OrganizationId { get; private set; }
    public PartyId PractitionerId { get; private set; }
    public Range<DateOnly> AgreementPeriod { get; private set; }
    public ImmutableList<Schedule> Schedules { get; private set; } = ImmutableList<Schedule>.Empty;
    IEnumerable<Schedule> IAgreementOptions.Schedules => Schedules;
}

public class AgreementManager : IAgreementOptions, IAgreementCreatorOptions
{
    public IAgreement Build()
    {
        return new Agreement(this);
    }

    public PartyId OrganizationId => Organization.Id;
    public IOrganization Organization { get; set; }
    public PartyId PractitionerId => Practitioner.Id;
    public IPerson Practitioner { get; set; }
    public Range<DateOnly> AgreementPeriod { get; set; }
    public List<Schedule> Schedules { get; } = new();
    IEnumerable<Schedule> IAgreementOptions.Schedules => Schedules;

    public AgreementManager AddSchedule(Schedule schedule)
    {
        Schedules.Add(schedule);
        return this;
    }

    public AgreementManager WithAgreementPeriod(Range<DateOnly> agreementPeriod)
    {
        AgreementPeriod = agreementPeriod;
        return this;
    }

    public AgreementManager WithOrganization(IOrganization organization)
    {
        Organization = organization;
        return this;
    }

    public AgreementManager WithPractitioner(IPerson practitioner)
    {
        Practitioner = practitioner;
        return this;
    }
}