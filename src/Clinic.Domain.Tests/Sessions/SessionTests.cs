using Clinic.Domain.Contracts.Parties.PartyRoles.Doctors;
using Clinic.Domain.Sessions;
using Clinic.Domain.Sessions.Exceptions;
using Clinic.Domain.Tests.Parties.PartyRoles.Doctors;
using FluentAssertions.Extensions;

namespace Clinic.Domain.Tests.Sessions;

public class SessionTests
{
    private readonly SessionTestBuilder _sutBuilder = new();

    [Fact]
    public async Task SetAppointment_Should_Add_Appointment_To_Session()
    {
        //arrange
        var appointmentTime = TestConstants.SomeDateTimeAtMonday.At(10, 0);

        //act
        var session =    await _sutBuilder.SetAppointment(new AppointmentOption(appointmentTime, TestConstants.SomePerson.Id,
            TestConstants.SomeValidDuration));

        //assert
        session.Appointments.Should().Contain(appointment => appointment.Time == appointmentTime &&
                                                         appointment.Patient == TestConstants.SomePerson.Id &&
                                                         appointment.DurationMinute == TestConstants.SomeValidDuration);
    }
    
    [Theory]
    [InlineData(SpecialityType.General, 10)]
    [InlineData(SpecialityType.General, 15)]
    [InlineData(SpecialityType.Specialist, 15)]
    [InlineData(SpecialityType.Specialist, 30)] 
    public async Task SetAppointment_Should_Add_Appointment_To_Session_For_InBoundaryValues(SpecialityType specialityType,
        int duration)
    {
        //arrange
        var appointmentTime = TestConstants.SomeDateTimeAtMonday.At(10, 0);
        _sutBuilder.SutBuilder.WithPractitioner(b =>
            b.WithPartyRoles(new DoctorTestBuilder().WithSpecialityType(specialityType).Build()));

        //act
        var session = await _sutBuilder.SetAppointment(new AppointmentOption(appointmentTime, TestConstants.SomePerson.Id,
            duration));

        //assert
        session.Appointments.Should().Contain(appointment => appointment.Time == appointmentTime &&
                                                         appointment.Patient == TestConstants.SomePerson.Id &&
                                                         appointment.DurationMinute == duration);
    }

    #region Exceptional Flow

    [Theory]
    [InlineData(SpecialityType.General, 9)]
    [InlineData(SpecialityType.General, 16)]
    [InlineData(SpecialityType.Specialist, 14)]
    [InlineData(SpecialityType.Specialist, 31)]
    public async Task SetAppointment_Should_Throw_Exception_If_Duration_Is_Not_Valid(SpecialityType specialityType,
        int duration)
    {
        //arrange
        var appointmentTime = TestConstants.SomeDateTimeAtMonday.At(10, 0);
        _sutBuilder.SutBuilder.WithPractitioner(b =>
            b.WithPartyRoles(new DoctorTestBuilder().WithSpecialityType(specialityType).Build()));

        //act
        var act = async () =>
            await _sutBuilder.SetAppointment(new AppointmentOption(appointmentTime,
                TestConstants.SomePerson.Id,
                duration));

        //assert
        await act.Should().ThrowAsync<AppointmentDurationIsInvalid>();
    }

    #endregion
}