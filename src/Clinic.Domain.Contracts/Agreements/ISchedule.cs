using System.Collections.Immutable;
using Core.SharedKernels;

namespace Clinic.Domain.Contracts.Agreements;

public interface ISchedule : IScheduleOption
{
    bool CoveredBy(IScheduleOption? firstOrDefault);
}