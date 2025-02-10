using Clinic.Domain.Contracts.Parties;
using Clinic.Domain.Tests.Agreements;
using FluentAssertions.Extensions;

namespace Clinic.Domain.Tests.Sessions;

public class SessionTests
{
    private readonly SessionTestBuilder _sutBuilder = new();
    [Fact]
    public async Task SetAppointment_Should_Add_Appointment_To_Session()
    {
        //arrange
        var sut = await _sutBuilder.Build();
        var appointmentTime=TestConstants.SomeDateTimeAtMonday.At(10, 0);
        
        //act
        sut.SetAppointment(appointmentTime, TestConstants.SomePerson, TestConstants.SomeValidDuration);
        
        //assert
        sut.Appointments.Should().Contain(appointment => appointment.Time == appointmentTime &&
                                                         appointment.Patient == TestConstants.SomePerson.Id &&
                                                         appointment.DurationMinute == TestConstants.SomeValidDuration);
    }
}