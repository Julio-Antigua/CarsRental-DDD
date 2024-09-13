using CarsRental.Application.Abstractions.Clock;
using CarsRental.Application.Abstractions.Messaging;
using CarsRental.Domain.Abstractions;
using CarsRental.Domain.Contracts.Repositories;
using CarsRental.Domain.Entities;
using CarsRental.Domain.Exceptions;
using CarsRental.Domain.ObjectsValue.Rentals;
using CarsRental.Domain.Services.Rentals;

namespace CarsRental.Application.Features.Rentals.Commands.RentalReserve
{
    internal sealed class RentalReserveCommandHandler : ICommandHandler<RentalReserveCommand, Guid>
    {

        private readonly IUserRepository _userRepository;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IRentalRepository _rentalRepository;
        private readonly PriceService _priceService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;

        public RentalReserveCommandHandler(
            IUserRepository userRepository, 
            IVehicleRepository carRepository, 
            IRentalRepository rentalRepository, 
            PriceService priceService, 
            IUnitOfWork unitOfWork,
            IDateTimeProvider dateTimeProvider)
        {
            _userRepository = userRepository;
            _vehicleRepository = carRepository;
            _rentalRepository = rentalRepository;
            _priceService = priceService;
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<Result<Guid>> Handle(RentalReserveCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId,cancellationToken);
            if(user is null)
            {
                return Result.Failure<Guid>(UserErrors.NotFound);
            }

            var vehicle = await _vehicleRepository.GetByIdAsync(request.CarId,cancellationToken);
            if(vehicle is null)
            {
                return Result.Failure<Guid>(VehicleErrors.NotFound);
            }

            var duration = DateRange.Create(request.StartDate, request.EndDate);
            if(await _rentalRepository.IsOverlappingAsync(vehicle, duration, cancellationToken))
            {
                return Result.Failure<Guid>(RentalErrors.Overlap);
            }

            var rental = Rental.Reserve(
                vehicle,
                user.Id,
                duration,
                _dateTimeProvider.currentTime,
                _priceService
            );

            _rentalRepository.Add( rental );
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return rental.Id;

        }
    }

}
