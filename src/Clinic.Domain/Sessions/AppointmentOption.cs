using Clinic.Domain.Contracts.Parties;
using Clinic.Domain.Contracts.Sessions;

namespace Clinic.Domain.Sessions;

public record AppointmentOption(DateTime Time, PartyId Patient, int DurationMinute) : IAppointmentOption
{
}