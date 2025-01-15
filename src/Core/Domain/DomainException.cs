namespace Core.Domain;

public class DomainException(string code, string message) : Exception(message)
{
    public string Code { get; } = code;
}