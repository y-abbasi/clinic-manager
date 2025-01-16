using System.Collections.Immutable;
using Clinic.Domain.Contracts.Agreements;
using Clinic.Domain.Contracts.Parties.PartyRoles.HealthCares;
using Clinic.Domain.Parties.PartyRoles.HealthCares;
using Core.SharedKernels;
using Newtonsoft.Json.Linq;

namespace Clinic.Domain.Tests.Parties.PartyRoles.HealthCares;

public abstract class HealthCareTestBuilder<TSelf> : PartyRoleTestBuilder<TSelf, HealthCare>, IAmWorkStation
    where TSelf : class, IPartyRoleTestBuilder<TSelf, HealthCare>
{
    public override string Code => HealthCare.RoleCode;

    public IEnumerable<ISchedule> WorkingSchedules => 
        Payload["WorkingSchedules"].ToObject<ImmutableList<ScheduleOption>>();
    public TSelf WithWorkingSchedules(IEnumerable<ISchedule> schedules)
    {
        Payload["WorkingSchedules"] = JToken.FromObject(schedules);
        return this;
    }

}

public class HealthCareTestBuilder : HealthCareTestBuilder<HealthCareTestBuilder>
{
    public HealthCareTestBuilder()
    {
        WithTitle(TestConstants.SomeName);
        WithWorkingSchedules([
            new Schedule(DayOfWeek.Friday, [
                new Range<TimeOnly>(new TimeOnly(8), new TimeOnly(12))
            ])
        ]);
    }
}