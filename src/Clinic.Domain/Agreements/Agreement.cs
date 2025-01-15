using System.Collections.Immutable;
using Clinic.Domain.Contracts.Agreements;
using Clinic.Domain.Contracts.Parties;
using Clinic.Domain.Contracts.Parties.Organizations;
using Clinic.Domain.Parties.PartyRoles.HealthCares;
using Core.Domain;
using Core.SharedKernels;

namespace Clinic.Domain.Agreements;

public class Agreement : AggregateRoot<AgreementId>, IAgreement
{
    public Agreement(IAgreementCreatorOptions  options)
    {
        CheckInvariants(options);
        SetupProperties(options);
    }

    private void CheckInvariants(IAgreementCreatorOptions options)
    {
        GuardAgainstInvalidOrganization(options.Organization);
    }

    private void GuardAgainstInvalidOrganization(IOrganization? organization)
    {
        if(organization == null)
            throw new DomainException("AGR-01", "Organization is required.");
        if(organization.PartyRoles.All(r => r is not HealthCare))
            throw new DomainException("AGR-02", "Organization should be health care.");
    }

    private void SetupProperties(IAgreementCreatorOptions options)
    {
        OrganizationId = options.OrganizationId;
        PractitionerId = options.PractitionerId;
        AgreementPeriod = options.AgreementPeriod;
        Schedules = options.Schedules.ToImmutableList();
    }

    public PartyId OrganizationId { get; private set; }
    public PartyId PractitionerId { get; private set; }
    public Range<DateOnly> AgreementPeriod { get; private set; }
    public ImmutableList<Schedule> Schedules { get; private set; } = ImmutableList<Schedule>.Empty;
    IEnumerable<Schedule> IAgreementOptions.Schedules => Schedules;
}