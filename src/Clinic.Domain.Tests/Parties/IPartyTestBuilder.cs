using Clinic.Domain.Contracts.Parties;

namespace Clinic.Domain.Tests.Parties;

public interface IPartyTestBuilder<TSelf, TAgg>
    where TSelf : IPartyTestBuilder<TSelf, TAgg>
    where TAgg : IParty
{
    TAgg Build();
}