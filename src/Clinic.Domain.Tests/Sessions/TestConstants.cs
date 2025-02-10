using Clinic.Domain.Contracts.Parties.People;
using Clinic.Domain.Parties.People;
using Clinic.Domain.Tests.Parties.People;

namespace Clinic.Domain.Tests.Sessions;

public class TestConstants
{
    public static IPerson SomePerson = new PersonTestBuilder().Build();
    public static DateTime SomeDateTimeAtMonday => new(2027, 1, 4, 0, 0, 0);

}