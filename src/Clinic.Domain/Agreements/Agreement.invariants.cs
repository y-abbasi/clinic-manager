using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.Intrinsics.Arm;
using Clinic.Domain.Contracts.Agreements;
using Clinic.Domain.Contracts.Parties.Organizations;
using Clinic.Domain.Contracts.Parties.PartyRoles.HealthCares;
using Clinic.Domain.Contracts.Parties.People;
using Clinic.Domain.Parties.PartyRoles.Doctors;
using Clinic.Domain.Parties.PartyRoles.HealthCares;
using Core.Domain;
using Newtonsoft.Json.Linq;

namespace Clinic.Domain.Agreements;

public partial class Agreement
{
    private void CheckInvariants(IAgreementCreatorOptions options)
    {
        GuardAgainstInvalidOrganization(options.Organization, out var healthCare);
        GuardAgainstInvalidPractitioner(options.Practitioner);
        GuardAgainstInvalidSchedules(
            options.Schedules.Select(s => new Schedule(s.DayOfWeek, s.WorkingTimes)).ToImmutableList(), healthCare);
    }

    private void GuardAgainstInvalidOrganization(IOrganization? organization, out HealthCare healthCare)
    {
        if (organization == null)
            throw new DomainException("AGR-01", "Organization is required.");
        healthCare = organization.PartyRoles.OfType<HealthCare>().FirstOrDefault()!;
        if (healthCare == null)
            throw new DomainException("AGR-02", "Organization should be health care.");
    }

    private void GuardAgainstInvalidPractitioner(IPerson practitioner)
    {
        if (practitioner == null)
            throw new DomainException("AGR-03", "Practitioner is required.");
        if (practitioner.PartyRoles.All(r => r is not Doctor))
            throw new DomainException("AGR-04", "Practitioner should be doctor.");
    }

    private void GuardAgainstInvalidSchedules(IList<Schedule> schedules, IAmWorkStation workStation)
    {
        if (schedules == null || !schedules.Any())
            throw new DomainException("AGR-05", "At least one schedule is required.");
        if (schedules.Any(schedule => !schedule.CoveredBy(workStation
                .WorkingSchedules.FirstOrDefault(w => w.DayOfWeek == schedule.DayOfWeek))))
            throw new DomainException("AGR-07", "Schedule should be in health care working times.");
    }
}