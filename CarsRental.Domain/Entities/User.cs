using CarsRental.Domain.Abstractions;
using CarsRental.Domain.Events.Users;
using CarsRental.Domain.ObjectsValue.Users;

namespace CarsRental.Domain.Entities
{
    public sealed class User : Entity
    {
        public User(
            Guid id,
            Name name,
            LastName lastName,
            Email email
            ) : base(id)
        {
            Name = name;
            LastName = lastName;
            Email = email;
        }

        public Name? Name { get; private set; }
        public LastName? LastName { get; private set; }
        public Email? Email { get; private set; }

        public static User Create(
            Name name,
            LastName lastName,
            Email email
            )
        {
            var user = new User(Guid.NewGuid(), name, lastName, email);
            user.RaiseDomainEvent(new UserCreatedDomainEvent(user.Id)); // estamos publicando el evento dentro del domain model (estamos haciendo un publish)
            return user;
        }
    }
}
