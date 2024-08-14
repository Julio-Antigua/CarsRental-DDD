using CarsRental.Domain.Entities;
using CarsRental.Domain.ObjectsValue.Rentals;

namespace CarsRental.Domain.Contracts.Repositories
{
    public interface IRentalRepository
    {
        Task<Rental?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<bool> IsOverlappingAsync(
            Car car,
            DateRange duration,
            CancellationToken cancellationToken = default
        );

        void Add(Rental rental);
    }
}
