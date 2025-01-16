using System.Collections.Immutable;
using Core.Domain;
using Core.SharedKernels;

namespace Clinic.Domain.Contracts.Agreements;

public record Schedule : ISchedule
{
    public Schedule(DayOfWeek DayOfWeek, ImmutableList<Range<TimeOnly>> WorkingTimes)
    {
        this.DayOfWeek = DayOfWeek;
        this.WorkingTimes = WorkingTimes;
        if (OverlapExistsOn())
            throw new DomainException("AGR-06", "Overlapping schedules are not supported.");
    }

    public DayOfWeek DayOfWeek { get; init; }
    public ImmutableList<Range<TimeOnly>> WorkingTimes { get; init; }

    public bool CoveredBy(IScheduleOption? schedule)
    {
        if (schedule is null) return false;
        return WorkingTimes.All(w => schedule.WorkingTimes.Any(t => t.InRange(w.Start) && t.InRange(w.End)));
    }

    private bool OverlapExistsOn()
    {
        Range<TimeOnly> first = WorkingTimes.First();
        foreach (var other in WorkingTimes.Skip(1))
        {
            if (first.HasOverlap(other)) return true;
        }

        return false;
    }

    public void Deconstruct(out DayOfWeek dayOfWeek, out ImmutableList<Range<TimeOnly>> workingTimes)
    {
        dayOfWeek = this.DayOfWeek;
        workingTimes = this.WorkingTimes;
    }
}