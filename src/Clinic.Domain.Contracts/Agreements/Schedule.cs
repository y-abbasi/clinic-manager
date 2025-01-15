using Core.SharedKernels;

namespace Clinic.Domain.Contracts.Agreements;

public record Schedule(DayOfWeek DayOfWeek, Range<TimeOnly> WorkingTime);