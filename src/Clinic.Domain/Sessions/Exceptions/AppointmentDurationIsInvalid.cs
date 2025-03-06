using Core.Domain;

namespace Clinic.Domain.Sessions.Exceptions;

public class AppointmentDurationIsInvalid() : DomainException("APP", "Appointment duration must be greater than or equal to minut")
{
}