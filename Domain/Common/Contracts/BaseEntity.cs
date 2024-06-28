

using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Common.Contracts
{
    public abstract class BaseEntity : BaseEntity<Guid>
    {
        protected BaseEntity()
        {
            Id = Guid.NewGuid();
        }
    }

    public abstract class BaseEntity<TId> : IEntity<TId>
    {
        public TId Id { get;  set; } = default!;

        //public List<DomainEvent> DomainEvents { get; } = new List<DomainEvent>();

        private readonly List<DomainEvent> _domainEvents = new();

        [NotMapped]
        public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        public void AddDomainEvent(DomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public void RemoveDomainEvent(DomainEvent domainEvent)
        {
            _domainEvents.Remove(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}
