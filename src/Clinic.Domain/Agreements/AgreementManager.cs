using System.Collections.Immutable;
using Clinic.Domain.Contracts.Agreements;
using Clinic.Domain.Contracts.Parties;
using Clinic.Domain.Contracts.Parties.Organizations;
using Clinic.Domain.Contracts.Parties.People;
using Core.SharedKernels;

namespace Clinic.Domain.Agreements;

public class AgreementManager : IAgreementOptions, IAgreementCreatorOptions
{
    public IAgreement Build()
    {
        return new Agreement(this);
    }

    public PartyId OrganizationId => Organization?.Id!;
    public IOrganization? Organization { get; set; }
    public PartyId PractitionerId => Practitioner?.Id!;
    public IPerson? Practitioner { get; set; }
    public Range<DateOnly> AgreementPeriod { get; set; }
    public List<ScheduleOption> Schedules { get; set; } = [];
    IEnumerable<IScheduleOption> IAgreementOptions.Schedules => Schedules;

    public AgreementManager AddSchedule(ScheduleOption schedule)
    {
        Schedules.Add(schedule);
        return this;
    }

    public AgreementManager WithAgreementPeriod(Range<DateOnly> agreementPeriod)
    {
        AgreementPeriod = agreementPeriod;
        return this;
    }

    public AgreementManager WithOrganization(IOrganization? organization)
    {
        Organization = organization;
        return this;
    }

    public AgreementManager WithPractitioner(IPerson? practitioner)
    {
        Practitioner = practitioner;
        return this;
    }

    public AgreementManager WithSchedules(List<ScheduleOption> schedules)
    {
        Schedules = schedules;
        return this;
    }
}

