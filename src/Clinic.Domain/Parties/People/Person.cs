using Clinic.Domain.Contracts.Parties;
using Clinic.Domain.Contracts.Parties.People;
using Clinic.Domain.Parties.Exceptions;
using Clinic.Domain.Parties.PartyRoles.Managers;

namespace Clinic.Domain.Parties.People;

public class Person : Party, IPerson
{
    public string FirstName { get; private set; } = default!;
    public string LastName { get; private set; } = default!;
    public virtual PartyType PartyType => PartyType.Person;

    private Person()
    {
    }

    public Person(IPersonOptions options, PartyRoleManager partyRoleManager)
        : base(options, partyRoleManager)
    {
        Id = PartyId.New();
        checkInvariants(options);
        updateProperties(options);
    }

    private void checkInvariants(IPersonOptions options)
    {
        if (options.FirstName == null)
            throw new FirstNameRequired();
    }

    private void updateProperties(IPersonOptions options)
    {
        FirstName = options.FirstName;
        LastName = options.LastName;
    }
}