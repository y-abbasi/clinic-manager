using System.Collections.Immutable;
using Clinic.Domain.Contracts.Agreements;
using Clinic.Domain.Contracts.Parties;
using Clinic.Domain.Contracts.Parties.PartyRoles.HealthCares;
using Clinic.Domain.Parties.Organizations;
using Core.Domain;

namespace Clinic.Domain.Parties.PartyRoles.HealthCares;

public class HealthCare : PartyRole, IAmWorkStation
{
    public static string RoleCode = "HealthCare";
    public override string Code => RoleCode;
    public override string Title { get; protected set; } = null!;

    public ImmutableList<Schedule> WorkingSchedules { get; protected set; }
    IEnumerable<IScheduleOption> IAmWorkStation.WorkingSchedules => WorkingSchedules;

    public override bool ApplicableToParty(IParty party)
    {
        return party is Organization;
    }

    private HealthCare()
    {
        
    }
    public HealthCare(HealthCareOptions options)
    {
        CheckInvariants(options);
        UpdateProperties(options);
    }

    private void CheckInvariants(HealthCareOptions options)
    {
        if (string.IsNullOrEmpty(options?.Title))
            throw new DomainException("HLC-01", "Title is required");
        if (options?.WorkingSchedules == null || options.WorkingSchedules.Count == 0)
            throw new DomainException("HLC-02", "At least one working schedule is required.");
    }

    private void UpdateProperties(HealthCareOptions options)
    {
        Title = options.Title;
        WorkingSchedules = options.WorkingSchedules
            .Select(w => new Schedule(w.DayOfWeek, w.WorkingTimes))
            .ToImmutableList();
    }
}