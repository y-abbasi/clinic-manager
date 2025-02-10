using Clinic.Domain.Contracts.Sessions;
using Clinic.Domain.Sessions;
using Clinic.Domain.Tests.Agreements;

namespace Clinic.Domain.Tests.Sessions;

public class SessionTestBuilder
{
    AgreementTestBuilder SutBuilder = new();

    public SessionTestBuilder()
    {
        var date = TestConstants.SomeDateTimeAtMonday;
        SutBuilder.ThereIsNotAnySessionFor(date);
    }

    public async Task<ISession> Build()
    {
        return await SutBuilder.GetOrCreateSession(TestConstants.SomeDateTimeAtMonday);
    }
}