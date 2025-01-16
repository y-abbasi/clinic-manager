using System.Collections.Immutable;
using Clinic.Domain.Contracts.Agreements;
using Clinic.Domain.Contracts.Parties;
using Clinic.Domain.Contracts.Parties.Organizations;
using Clinic.Domain.Parties.PartyRoles.HealthCares;
using Core.Domain;
using Core.SharedKernels;

namespace Clinic.Domain.Agreements;

public partial class Agreement : AggregateRoot<AgreementId>, IAgreement
{
    public Agreement(IAgreementCreatorOptions options)
    {
        CheckInvariants(options);
        SetupProperties(options);
        //CheckPostConditions();
    }

    private void SetupProperties(IAgreementCreatorOptions options)
    {
        OrganizationId = options.OrganizationId;
        PractitionerId = options.PractitionerId;
        AgreementPeriod = options.AgreementPeriod;
        Schedules = options.Schedules.GroupBy(s => s.DayOfWeek)
            .Select(a =>
                new Schedule(a.Key, 
                    a.SelectMany(s => s.WorkingTimes).ToImmutableList()))
            .ToImmutableList();
    }

    public PartyId OrganizationId { get; private set; }
    public PartyId PractitionerId { get; private set; }
    public Range<DateOnly> AgreementPeriod { get; private set; }
    private ImmutableList<Schedule> Schedules { get; set; } = ImmutableList<Schedule>.Empty;
    IEnumerable<ISchedule> IAgreementOptions.Schedules => Schedules;
}