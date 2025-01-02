using Clinic.Domain.Contracts.Parties;
using Clinic.Domain.Contracts.Parties.People;
using Clinic.Domain.Parties.People;

namespace Clinic.Domain.Tests.Parties;

public abstract class PartyTestBuilder : IPartyTestBuilder, IPartyOptions
{
    private protected PartyTestBuilder()
    {
    }

    public abstract IParty Build();
}

public interface IPartyTestBuilder
{
    IParty Build();
}