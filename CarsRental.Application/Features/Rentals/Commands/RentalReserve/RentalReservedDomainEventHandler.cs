using CarsRental.Application.Abstractions.Emails;
using CarsRental.Domain.Contracts.Repositories;
using CarsRental.Domain.Events.Rentals;
using MediatR;

namespace CarsRental.Application.Features.Rentals.Commands.RentalReserve
{
    internal sealed class RentalReservedDomainEventHandler 
        : INotificationHandler<RentalReservedDomainEvent>
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;

        public RentalReservedDomainEventHandler(
            IRentalRepository rentalRepository, 
            IUserRepository userRepository,
            IEmailService emailService)
        {
            _rentalRepository = rentalRepository;
            _userRepository = userRepository;
            _emailService = emailService;
        }

        public async Task Handle(
            RentalReservedDomainEvent notification, 
            CancellationToken cancellationToken)
        {
            var rental = await _rentalRepository.GetByIdAsync(notification.rentalId,cancellationToken);
            if (rental is null) { return;}

            var user = await _userRepository.GetByIdAsync(rental.UserId,cancellationToken);
            if (user is null) { return; }

            await _emailService.SendAsync(
                user.Email!,
                "Rental Reserved",
                "You must confirm this reservation otherwise it will be lost."
            );
        }
    }
}
