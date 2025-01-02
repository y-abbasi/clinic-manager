namespace Clinic.Domain.Contracts.Patients;

public record PatientId(Guid Value)
{
    public static PatientId New()
    {
        return new(Guid.NewGuid());
    }
}