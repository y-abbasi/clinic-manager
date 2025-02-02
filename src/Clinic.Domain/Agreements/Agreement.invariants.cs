using System.Collections.Immutable;
using System.Runtime.Intrinsics.Arm;
using Clinic.Domain.Contracts.Agreements;
using Clinic.Domain.Contracts.Parties.Organizations;
using Clinic.Domain.Contracts.Parties.People;
using Clinic.Domain.Parties.PartyRoles.Doctors;
using Clinic.Domain.Parties.PartyRoles.HealthCares;
using Core.Domain;

namespace Clinic.Domain.Agreements;

public partial class Agreement
{
    private void CheckInvariants(IAgreementCreatorOptions options)
    {
        GuardAgainstInvalidOrganization(options.Organization);
        GuardAgainstInvalidPractitioner(options.Practitioner);
        GuardAgainstInvalidSchedules(options.Schedules.ToImmutableList());
    }

    private void GuardAgainstInvalidOrganization(IOrganization? organization)
    {
        if (organization == null)
            throw new DomainException("AGR-01", "Organization is required.");
        if (organization.PartyRoles.All(r => r is not HealthCare))
            throw new DomainException("AGR-02", "Organization should be health care.");
    }

    private void GuardAgainstInvalidPractitioner(IPerson practitioner)
    {
        if (practitioner == null)
            throw new DomainException("AGR-03", "Practitioner is required.");
        if (practitioner.PartyRoles.All(r => r is not Doctor))
            throw new DomainException("AGR-04", "Practitioner should be doctor.");
    }

    private void GuardAgainstInvalidSchedules(IList<ISchedule> schedules)
    {
        if (schedules == null || !schedules.Any())
            throw new DomainException("AGR-05", "At least one schedule is required.");
    }
    
}