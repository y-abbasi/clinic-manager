namespace Clinic.Domain.Contracts.Parties.People;

public interface IPersonOptions : IPartyOptions
{
    string FirstName { get; }
    string LastName { get; }
}