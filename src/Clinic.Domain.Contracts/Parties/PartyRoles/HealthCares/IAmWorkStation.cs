using Clinic.Domain.Contracts.Agreements;

namespace Clinic.Domain.Contracts.Parties.PartyRoles.HealthCares;

public interface IAmWorkStation
{
    IEnumerable<IScheduleOption> WorkingSchedules { get; }
}