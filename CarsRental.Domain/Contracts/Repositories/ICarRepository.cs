using CarsRental.Domain.Entities;

namespace CarsRental.Domain.Contracts.Repositories
{
    public interface ICarRepository
    {
        Task<Car?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default); 
    }
}
