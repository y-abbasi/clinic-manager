namespace Core.Domain;

public abstract class AggregateRoot<TKey> : IAggregateRoot<TKey>
{
    public TKey Id { get; protected init; }
}
