using CarsRental.Domain.Entities;

namespace CarsRental.Domain.Contracts.Repositories
{
    public interface IVehicleRepository
    {
        Task<Vehicle?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default); 
    }
}
