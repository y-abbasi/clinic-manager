namespace Clinic.Domain.Contracts.Parties;

public record PartyId(Guid Value)
{
    public static PartyId New()
    {
        return new(Guid.NewGuid());
    }
}