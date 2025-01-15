using Clinic.Domain.Contracts.Parties.Organizations;
using Clinic.Domain.Contracts.Parties.People;

namespace Clinic.Domain.Contracts.Agreements;

public interface IAgreementCreatorOptions : IAgreementOptions
{
    IOrganization Organization { get; }
    IPerson Practitioner { get; }
    
}