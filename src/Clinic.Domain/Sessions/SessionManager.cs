using Clinic.Domain.Contracts.Parties;
using Clinic.Domain.Contracts.Sessions;

namespace Clinic.Domain.Sessions;

public class SessionManager : ISessionOption
{
    public PartyId OrganizationId { get; set; }
    public PartyId PractitionerId { get; set; }
    public DateOnly Date { get; set; }
    public SessionManager WithOrganization(
        PartyId organizationId)
    {
        OrganizationId = organizationId;
        return this;
    }
    public SessionManager WithPractitioner(
        PartyId practitionerId)
    {
        PractitionerId = practitionerId;
        return this;
    }

    public SessionManager WithDate(DateOnly date)
    {
        Date = date ;
        return this;
    }
}