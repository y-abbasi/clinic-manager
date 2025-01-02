namespace Core.Domain;

public interface IAggregateRoot<out TKey>
{
    TKey Id { get; }
}