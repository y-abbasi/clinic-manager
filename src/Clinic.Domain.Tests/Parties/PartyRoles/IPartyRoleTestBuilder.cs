using Clinic.Domain.Contracts.Parties.PartyRoles;

namespace Clinic.Domain.Tests.Parties.PartyRoles;

public interface IPartyRoleTestBuilder<out TBuilder, out TEntity>
{
    string Code { get; }
    string Title { get; }
    TEntity Build();
    IPartyRoleOptions BuildOptions();
}