using Clinic.Domain.Contracts.Parties;
using Core.SharedKernels;

namespace Clinic.Domain.Contracts.Agreements;

public interface IAgreementOptions
{
    PartyId OrganizationId { get; }
    PartyId PractitionerId { get; }
    Range<DateOnly> AgreementPeriod { get; }
    IEnumerable<IScheduleOption> Schedules { get; }
}