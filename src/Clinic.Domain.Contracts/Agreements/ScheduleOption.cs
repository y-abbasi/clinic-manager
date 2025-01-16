using System.Collections.Immutable;
using Core.SharedKernels;

namespace Clinic.Domain.Contracts.Agreements;

public record ScheduleOption(DayOfWeek DayOfWeek, ImmutableList<Range<TimeOnly>> WorkingTimes)
    : ISchedule
{
}