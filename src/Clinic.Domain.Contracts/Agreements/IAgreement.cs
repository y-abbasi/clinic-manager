using Core.Domain;

namespace Clinic.Domain.Contracts.Agreements;

public interface IAgreement : IAggregateRoot<AgreementId>, IAgreementOptions
{
}