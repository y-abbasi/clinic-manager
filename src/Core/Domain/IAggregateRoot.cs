namespace Core.Domain;

public interface IAggregateRoot<out TKey>:IEntity<TKey>
{
}