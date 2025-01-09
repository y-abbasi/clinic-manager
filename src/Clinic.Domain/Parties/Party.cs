using System.Collections.Immutable;
using Clinic.Domain.Contracts.Parties;
using Clinic.Domain.Contracts.Parties.PartyRoles;
using Clinic.Domain.Parties.PartyRoles.Managers;
using Core.Domain;
using Newtonsoft.Json;

namespace Clinic.Domain.Parties;

public abstract class Party : AggregateRoot<PartyId>, IParty
{
    protected Party()
    {
    }
    
    protected Party(IPartyOptions options, PartyRoleManager partyRoleManager)
    {
        PartyRoles = PartyRoles.AddRange(options.PartyRoles.Select(r => partyRoleManager.Build(r.Code, r)));
        if (PartyRoles.Any(r => r.AcceptedByPartyType(this)))
            throw new Exception();
    }

    public ImmutableList<IPartyRole> PartyRoles { get; protected set; } = ImmutableList<IPartyRole>.Empty;
    IEnumerable<IPartyRoleOptions> IPartyOptions.PartyRoles => PartyRoles;
}