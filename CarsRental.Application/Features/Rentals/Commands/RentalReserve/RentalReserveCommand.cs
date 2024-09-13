using CarsRental.Application.Abstractions.Messaging;

namespace CarsRental.Application.Features.Rentals.Commands.RentalReserve
{
    public record RentalReserveCommand(Guid CarId,Guid UserId,DateOnly StartDate, DateOnly EndDate) : ICommand<Guid>;
}
