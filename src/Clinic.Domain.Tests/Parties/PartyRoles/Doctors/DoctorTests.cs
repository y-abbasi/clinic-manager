using Clinic.Domain.Contracts.Parties;
using Clinic.Domain.Contracts.Parties.PartyRoles.Doctors;
using Clinic.Domain.Contracts.Sessions;
using Clinic.Domain.Parties.Exceptions;
using Clinic.Domain.Parties.PartyRoles.Doctors;
using Clinic.Domain.Sessions.Exceptions;
using Core.Domain;
using Core.SharedKernels;
using FluentAssertions;
using FluentAssertions.Extensions;
using NSubstitute;
using Xunit;

namespace Clinic.Domain.Tests.Parties.PartyRoles.Doctors;

public class DoctorTests : PartyRoleTests<DoctorTestBuilder, Doctor>
{
    private readonly IClientAppointmentService _patientAppointmentService;
    private readonly ISession _session;

    public DoctorTests()
    {
        _patientAppointmentService = Substitute.For<IClientAppointmentService>();
        _session = Substitute.For<ISession>();
    }

    protected override DoctorTestBuilder CreateSutBuilder() => new();

    [Fact]
    public async Task ValidateAppointment_WhenPatientHasLessThanTwoAppointments_ShouldNotThrowException()
    {
        // Arrange
        var appointment = Substitute.For<IAppointmentOption>();
        appointment.Patient.Returns(Sessions.TestConstants.SomePerson.Id);
        appointment.Time.Returns(Sessions.TestConstants.SomeDateTimeAtMonday.At(10, 0));
        appointment.DurationMinute.Returns(Sessions.TestConstants.SomeValidDuration);

        _patientAppointmentService
            .GetClientAppointmentsCountAsync(Arg.Any<PartyId>(), Arg.Any<DateOnly>())
            .Returns(1);

        var doctor = SutBuilder
            .WithSpecialityType(SpecialityType.General)
            .Build();

        // Act
        var act = () => doctor.ValidateAppointment(_session, appointment, _patientAppointmentService);

        // Assert
        await act.Should().NotThrowAsync();
    }

    [Fact]
    public async Task ValidateAppointment_WhenPatientHasTwoAppointments_ShouldThrowException()
    {
        // Arrange
        var appointment = Substitute.For<IAppointmentOption>();
        appointment.Patient.Returns(Sessions.TestConstants.SomePerson.Id);
        appointment.Time.Returns(Sessions.TestConstants.SomeDateTimeAtMonday.At(10, 0));
        appointment.DurationMinute.Returns(Sessions.TestConstants.SomeValidDuration);

        _patientAppointmentService
            .GetClientAppointmentsCountAsync(Arg.Any<PartyId>(), Arg.Any<DateOnly>())
            .Returns(2);

        var doctor = SutBuilder
            .WithSpecialityType(SpecialityType.General)
            .Build();

        // Act
        var act = () => doctor.ValidateAppointment(_session, appointment, _patientAppointmentService);

        // Assert
        var exception = await act.Should().ThrowAsync<ClientDailyAppointmentLimitExceeded>();
        exception.Which.Code.Should().Be("APT-01");
        exception.Which.Message.Should().Be("مشتری نمی‌تواند بیش از ۲ قرار ملاقات در یک روز داشته باشد.");
    }

    [Theory]
    [InlineData(SpecialityType.General, 9)]
    [InlineData(SpecialityType.General, 16)]
    [InlineData(SpecialityType.Specialist, 14)]
    [InlineData(SpecialityType.Specialist, 31)]
    public async Task ValidateAppointment_WhenDurationIsInvalid_ShouldThrowException(SpecialityType specialityType, int duration)
    {
        // Arrange
        var appointment = Substitute.For<IAppointmentOption>();
        appointment.Patient.Returns(Sessions.TestConstants.SomePerson.Id);
        appointment.Time.Returns(Sessions.TestConstants.SomeDateTimeAtMonday.At(10, 0));
        appointment.DurationMinute.Returns(duration);

        var doctor = SutBuilder
            .WithSpecialityType(specialityType)
            .Build();

        // Act
        var act = () => doctor.ValidateAppointment(_session, appointment, _patientAppointmentService);

        // Assert
        await act.Should().ThrowAsync<AppointmentDurationIsInvalid>();
    }
}