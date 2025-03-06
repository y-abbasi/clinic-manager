using System.Collections.Immutable;
using Clinic.Domain.Contracts.Agreements;

namespace Clinic.Domain.Contracts.Parties.PartyRoles.HealthCares;

public interface IHealthCareOptions : IPartyRoleOptions, IAmWorkStation
{
}

public class HealthCareOptions : IHealthCareOptions
{
    public string Code { get; set; } = default!;
    public string Title { get; set; } = default!;
    public ImmutableList<ScheduleOption> WorkingSchedules { get; set; }
    IEnumerable<IScheduleOption> IAmWorkStation.WorkingSchedules => WorkingSchedules;
}