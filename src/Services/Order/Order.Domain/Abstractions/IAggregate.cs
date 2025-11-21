namespace Order.Domain.Abstractions;

public interface IAggregate : IEntity
{
    IReadOnlyList<IDomainEvent> DomainEvents { get; }
    IDomainEvent[] ClearDomainEvents();
    void AddDomainEvent(IDomainEvent domainEvent);
}

public interface IAggregate<T> : IAggregate, IEntity<T>
{

}