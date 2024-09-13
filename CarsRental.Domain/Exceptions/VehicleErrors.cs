using CarsRental.Domain.Abstractions;

namespace CarsRental.Domain.Exceptions
{
    public static class VehicleErrors
    {
        public static Error NotFound = new(
            "Vehicle.NotFound",
            "There is no vehicle with this id"
        );
    }
}
