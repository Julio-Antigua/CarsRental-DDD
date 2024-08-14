using CarsRental.Domain.Abstractions;
using CarsRental.Domain.Enums;
using CarsRental.Domain.ObjectsValue.Cars;
using CarsRental.Domain.Shared;

namespace CarsRental.Domain.Entities
{
    //sealed no permitira que otras clases puedan heredar de esta
    public sealed class Car : Entity
    {
        public Car(
            Guid id,
            Model model,
            Vin vin,
            Currency price,
            Currency maintenance,
            DateTime? lastRentalDate,
            List<Accessories> accessories,
            Address? address
            ) : base(id)
        {
            Model = model;
            Vin = vin;
            Price = price;
            Maintenance = maintenance;
            LastRentalDate = lastRentalDate;
            Accessories = accessories;
            Address = address;
        }

        public Model? Model { get; private set; } // private solo permitira quese modifique el valor de esta propiedad a nivel de esta clase
        public Vin? Vin { get; private set; }
        public Address? Address { get; private set; }
        public Currency? Price { get; private set; }
        public Currency? Maintenance { get; private set; }
        public DateTime? LastRentalDate { get; internal set; }
        public List<Accessories> Accessories { get; private set; } = new();
        
    }
}
