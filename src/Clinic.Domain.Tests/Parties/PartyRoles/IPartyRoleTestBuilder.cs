namespace Clinic.Domain.Tests.Parties.PartyRoles;

public interface IPartyRoleTestBuilder<out TBuilder, out TEntity>
{
    TEntity Build();
}