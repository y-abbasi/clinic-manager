using Clinic.Domain.Contracts.Patients;
using Core.Domain;

namespace Clinic.Domain.Patients;

public class Patient : AggregateRoot<PatientId>, IPatient
{
    public string FirstName { get; private set; } = default!;
    public string LastName { get; private set; } = default!;

    private Patient()
    {
    }

    public Patient(IPatientOptions options)
    {
        Id = PatientId.New();
        updateProperties(options);
    }

    private void updateProperties(IPatientOptions options)
    {
        FirstName = options.FirstName;
        LastName = options.LastName;
    }
}