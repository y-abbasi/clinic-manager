using System.Collections.Immutable;
using Core.SharedKernels;

namespace Clinic.Domain.Contracts.Agreements;

public interface ISchedule
{
    DayOfWeek DayOfWeek { get; init; }
    ImmutableList<Range<TimeOnly>> WorkingTimes { get; init; }
}