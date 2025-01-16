using System.Collections.Immutable;
using Core.SharedKernels;

namespace Clinic.Domain.Contracts.Agreements;

public interface IScheduleOption
{
    DayOfWeek DayOfWeek { get; init; }
    ImmutableList<Range<TimeOnly>> WorkingTimes { get; init; }

}

public record ScheduleOption(DayOfWeek DayOfWeek, ImmutableList<Range<TimeOnly>> WorkingTimes) : IScheduleOption
{
}