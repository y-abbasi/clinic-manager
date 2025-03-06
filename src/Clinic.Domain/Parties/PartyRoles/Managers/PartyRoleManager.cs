using Clinic.Domain.Contracts.Parties.PartyRoles;
using Clinic.Domain.Contracts.Parties.PartyRoles.HealthCares;
using Clinic.Domain.Parties.PartyRoles.HealthCares;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using BindingFlags = System.Reflection.BindingFlags;

namespace Clinic.Domain.Parties.PartyRoles.Managers;

public class PartyRoleManager //: IPartyRoleOptions
{
    private static Dictionary<string, IPartyRoleBuilder> mapper = new();

    static PartyRoleManager()
    {
        mapper = typeof(PartyRoleManager).Assembly
            .DefinedTypes
            .Where(t => t.IsClass && t.IsAssignableTo(typeof(IPartyRoleBuilder)))
            .Select(t => (IPartyRoleBuilder)Activator.CreateInstance(t)!)
            .ToDictionary(role => role!.Code);
    }


    public IPartyRole Build(string code, JObject payload)
    {
        var options = BuildOptions(code, payload);
        return Build(code, options);
    }

    public IPartyRole Build(string code, IPartyRoleOptions options)
    {
        var builder = mapper[code];
        // var ctor = builder.GetPartyRoleType().GetConstructors().FirstOrDefault();
        // if (ctor == null)
        //     throw new Exception($"Party role code {builder.GetPartyRoleType().FullName} not found");
        // var partyRole = ctor
        //     .Invoke([options]);

        var partyRole = Activator.CreateInstance(builder.GetPartyRoleType(), options)!;
        return (IPartyRole)partyRole;
    }

    public IPartyRoleOptions BuildOptions(string code, JObject payload)
    {
        payload["code"] = code;
        return (IPartyRoleOptions)payload.ToObject(mapper[code].GetPartyRoleOptionType())!;
    }
}