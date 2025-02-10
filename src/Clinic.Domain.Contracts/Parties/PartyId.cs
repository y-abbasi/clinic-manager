namespace Clinic.Domain.Contracts.Parties;

public record PartyId(Guid Value)
{
    public PartyId() : this(Guid.NewGuid())
    {
    }
}