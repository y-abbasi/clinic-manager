using Core.Domain;

namespace Clinic.Domain.Contracts.Patients;

public interface IPatient : IAggregateRoot<PatientId>, IPatientOptions
{
}