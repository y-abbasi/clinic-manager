using Clinic.Domain.Contracts.Parties;
using Clinic.Domain.Contracts.Parties.PartyRoles;
using Clinic.Domain.Contracts.Parties.People;
using Clinic.Domain.Parties.People;
using Clinic.Domain.Tests.Parties.PartyRoles.Doctors;
using Clinic.Domain.Tests.Parties.PartyRoles.HealthCares;
using Clinic.Domain.Tests.Parties.People;

namespace Clinic.Domain.Tests.Parties;

public abstract class PartyTestBuilder<TSelf, TAgg> : IPartyTestBuilder<TSelf, TAgg>, IPartyOptions
    where TSelf : class, IPartyTestBuilder<TSelf, TAgg>
    where TAgg : IParty
{
    private protected PartyTestBuilder()
    {
    }

    public List<IPartyRoleOptions> PartyRoles { get; } = new();
    IEnumerable<IPartyRoleOptions> IPartyOptions.PartyRoles => PartyRoles;

    public TSelf IsDoctor()
    {
        PartyRoles.Add(new DoctorTestBuilder().BuildOptions());
        return this;
    }

    public TSelf WithHealthCareRole(Action<HealthCareTestBuilder>? configure = null)
    {
        var builder = new HealthCareTestBuilder();
        configure?.Invoke(builder);
        PartyRoles.Add(builder.BuildOptions());
        return this;
    }

    public TSelf WithPartyRoles(params IPartyRoleOptions[] partyRoles)
    {
        partyRoles.ToList().ForEach(partyRole => PartyRoles.Add(partyRole));
        return this;
    }

    public TSelf WithoutAnyRole()
    {
        PartyRoles.Clear();
        return this;
    }

    public abstract TAgg Build();
    public static implicit operator TSelf(PartyTestBuilder<TSelf, TAgg> builder) => (builder as TSelf)!;
}