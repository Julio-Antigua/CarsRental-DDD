namespace CarsRental.Domain.Abstractions
{
    public abstract class Entity
    {
        private readonly List<IDomainEvent> _domainEvents = new();
        protected Entity(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; init; } // init hace que este propiedad solo tenga un inicializador unico y nunca cambie 

        public IReadOnlyList<IDomainEvent> GetDomainEvents { get { return _domainEvents.ToList(); } }
        public void ClearDomainEvents() { _domainEvents.Clear(); }
        protected void RaiseDomainEvent(IDomainEvent domainEvents) { _domainEvents.Add(domainEvents); }
    }
}
