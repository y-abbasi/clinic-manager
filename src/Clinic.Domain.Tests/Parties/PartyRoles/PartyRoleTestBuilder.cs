using System.Reflection;
using Clinic.Domain.Contracts.Agreements;
using Clinic.Domain.Contracts.Parties.PartyRoles;
using Clinic.Domain.Parties.PartyRoles;
using Clinic.Domain.Parties.PartyRoles.Managers;
using Clinic.Domain.Tests.Parties.PartyRoles.HealthCares;
using Core.SharedKernels;
using Newtonsoft.Json.Linq;

namespace Clinic.Domain.Tests.Parties.PartyRoles;

public abstract class PartyRoleTestBuilder<TBuilder, TEntity> : IPartyRoleTestBuilder<TBuilder, TEntity>
    where TBuilder : class, IPartyRoleTestBuilder<TBuilder, TEntity>
    where TEntity : IPartyRole
{
    private readonly PartyRoleManager _manager = new PartyRoleManager();
    public abstract string Code { get; }

    public PartyRoleTestBuilder()
    {
    }

    protected JObject Payload { get; } = new();
    public string Title => Payload["Title"].ToObject<string>();

    public TBuilder WithTitle(string title)
    {
        Payload["Title"] = title;
        return this;
    }

    public TEntity Build()
    {
        try
        {
            return (TEntity)_manager.Build(Code, Payload);
        }
        catch (TargetInvocationException e)
        {
            throw e.InnerException!;
        }
    }

    public IPartyRoleOptions BuildOptions()
    {
        return _manager.BuildOptions(Code, Payload);
    }

    public static implicit operator TBuilder(PartyRoleTestBuilder<TBuilder, TEntity> builder) =>
        (builder as TBuilder)!;
}