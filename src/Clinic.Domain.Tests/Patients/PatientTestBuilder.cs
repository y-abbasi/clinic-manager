using Clinic.Domain.Contracts.Patients;
using Clinic.Domain.Patients;

namespace Clinic.Domain.Tests.Patients;

internal class PatientTestBuilder : IPatientOptions
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public PatientTestBuilder()
    {
        FirstName = TestConstants.SomeName;
        LastName = TestConstants.SomeLastName;
    }
    public Patient Build()
    {
        return new(this);
    }
}