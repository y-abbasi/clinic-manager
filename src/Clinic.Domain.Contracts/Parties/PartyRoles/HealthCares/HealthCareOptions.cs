using System.Collections.Immutable;
using Clinic.Domain.Contracts.Agreements;

namespace Clinic.Domain.Contracts.Parties.PartyRoles.HealthCares;

public class HealthCareOptions : IPartyRoleOptions, IAmWorkStation
{
    public string Code { get; set; } = default!;
    public string Title { get; set; } = default!;
    public ImmutableList<ScheduleOption> WorkingSchedules { get; set; }
    IEnumerable<ISchedule> IAmWorkStation.WorkingSchedules => WorkingSchedules;
}