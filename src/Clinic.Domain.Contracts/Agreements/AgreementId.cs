namespace Clinic.Domain.Contracts.Agreements;

public record AgreementId(Guid Value)
{
    public AgreementId() : this(Guid.NewGuid())
    {
    }
}