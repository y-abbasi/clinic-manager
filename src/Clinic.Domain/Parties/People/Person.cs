using Clinic.Domain.Contracts.Parties;
using Clinic.Domain.Contracts.Parties.People;
using Clinic.Domain.Parties.PartyRoles.Managers;

namespace Clinic.Domain.Parties.People;

public class Person : Party, IPerson
{
    public string FirstName { get; private set; } = default!;
    public string LastName { get; private set; } = default!;

    private Person()
    {
    }

    public Person(IPersonOptions options, PartyRoleManager partyRoleManager)
        : base(options, partyRoleManager)
    {
        Id = PartyId.New();
        updateProperties(options);
    }

    private void updateProperties(IPersonOptions options)
    {
        FirstName = options.FirstName;
        LastName = options.LastName;
    }
}