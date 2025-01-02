using Clinic.Domain.Contracts.Parties;
using Clinic.Domain.Contracts.Parties.People;

namespace Clinic.Domain.Parties.People;

public class Person : Party, IPerson
{
    public string FirstName { get; private set; } = default!;
    public string LastName { get; private set; } = default!;

    private Person()
    {
    }

    public Person(IPersonOptions options)
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