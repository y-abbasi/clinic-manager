using Clinic.Domain.Contracts.Parties;
using Clinic.Domain.Contracts.Parties.PartyRoles;

namespace Clinic.Domain.Tests.Parties;

public interface IPartyTestBuilder<TSelf, TAgg>
    where TSelf : IPartyTestBuilder<TSelf, TAgg>
    where TAgg : IParty
{
    TSelf WithPartyRoles(params IPartyRoleOptions[] partyRoles);
    TAgg Build();
}