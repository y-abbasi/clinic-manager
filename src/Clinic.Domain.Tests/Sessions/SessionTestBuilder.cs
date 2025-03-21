using Clinic.Domain.Contracts.Agreements;
using Clinic.Domain.Contracts.Parties;
using Clinic.Domain.Contracts.Sessions;
using Clinic.Domain.Tests.Agreements;
using NSubstitute;

namespace Clinic.Domain.Tests.Sessions;

public class SessionTestBuilder
{
    public AgreementTestBuilder SutBuilder;
    ISessionService SessionService => Substitute.For<ISessionService>();
    IPartyService PartyService = Substitute.For<IPartyService>();
    IClientAppointmentService PatientAppointmentService = Substitute.For<IClientAppointmentService>();
    public IAgreement Sut;

    public SessionTestBuilder()
    {
        var date = TestConstants.SomeDateTimeAtMonday;
        SutBuilder = new AgreementTestBuilder();
        SutBuilder.ThereIsNotAnySessionFor(date);
    }


    public async Task<ISession> Build()
    {
        Sut = SutBuilder.Build();
        return await SutBuilder.GetOrCreateSession( TestConstants.SomeDateTimeAtMonday);
    }

    public async Task<ISession> SetAppointment(IAppointmentOption option)
    {
        return await SutBuilder.SetAppointmentAsync(option);
        return await Sut.SetAppointmentAsync(option, SessionService, PartyService, PatientAppointmentService);
    }
}