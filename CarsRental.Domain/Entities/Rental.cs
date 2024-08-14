using CarsRental.Domain.Abstractions;
using CarsRental.Domain.Enums;
using CarsRental.Domain.Events.Rentals;
using CarsRental.Domain.Exceptions;
using CarsRental.Domain.ObjectsValue.Rentals;
using CarsRental.Domain.Services.Rentals;
using CarsRental.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace CarsRental.Domain.Entities
{
    public sealed class Rental : Entity
    {
        private Rental(
            Guid id,
            Guid carId,
            Guid userId,
            DateRange duration,
            Currency pricePerPeriod,
            Currency maintenancePrice,
            Currency priceForAccesories,
            Currency totalPrice,
            RentalStatus status,
            DateTime creationDate
            ) : base(id)
        {
            CarId = carId;
            UserId = userId;
            Duration = duration;
            PricePerPeriod = pricePerPeriod;
            MaintenancePrice = maintenancePrice;
            PriceForAccessories = priceForAccesories;
            TotalPrice = totalPrice;
            Status = status;
            CreationDate = creationDate;
        }

        public Guid CarId { get; private set; }
        public Guid UserId { get; private set; }
        public Currency? PricePerPeriod { get; private set; }
        public Currency? MaintenancePrice { get; private set; }
        public Currency? PriceForAccessories { get; private set; }
        public Currency? TotalPrice { get; private set; }
        public RentalStatus Status { get; private set; }
        public DateRange Duration { get; private set; }
        public DateTime? CreationDate { get; private set; }
        public DateTime? ConfirmationDate { get; private set; }
        public DateTime? DenialDate { get; private set; }
        public DateTime? DateCompleted { get; private set; }
        public DateTime? CancellationDate { get; private set; }

        public static Rental Reserve(
            Car car,
            Guid userId,
            DateRange duration,
            DateTime creationDate,
            PriceService priceService
        )
        {
            var detailPrice = priceService.CalculatePrice(
                car,
                duration
            );
            var rental = new Rental(
                Guid.NewGuid(),
                car.Id,
                userId,
                duration,
                detailPrice.PricePerPeriod,
                detailPrice.MaintenancePrice,
                detailPrice.PriceForAccessories,
                detailPrice.TotalPrice,
                RentalStatus.Reserved,
                creationDate
            );
            rental.RaiseDomainEvent(new RentalReservedDomainEvent(rental.Id!));
            car.LastRentalDate = creationDate;
            return rental;
        }

        public Result Confirmed(DateTime utcNow)
        {
            if (Status != RentalStatus.Reserved)
            {
                return Result.Failure(RentalErrors.NotReserved);
            }

            Status = RentalStatus.Confirmed;
            ConfirmationDate = utcNow;

            RaiseDomainEvent(new RentalConfirmedDomainEvent(Id));
            return Result.Success();
        }

        public Result Decline(DateTime utcNow)
        {
            if (Status != RentalStatus.Reserved)
            {
                return Result.Failure(RentalErrors.NotReserved);
            }

            Status = RentalStatus.Decline;
            DenialDate = utcNow;

            RaiseDomainEvent(new RentalDeclinedDomainEvent(Id));
            return Result.Success();
        }

        public Result Cancel(DateTime utcNow)
        {
            if (Status != RentalStatus.Confirmed)
            {
                return Result.Failure(RentalErrors.NotConfirmed);
            }

            var currentDate = DateOnly.FromDateTime(utcNow);
            if (currentDate > Duration!.Start) { return Result.Failure(RentalErrors.AlreadyStarted); }

            Status = RentalStatus.Canceled;
            CancellationDate = utcNow;

            RaiseDomainEvent(new RentalCanceledDomainEvent(Id));
            return Result.Success();
        }

        public Result Complete(DateTime utcNow)
        {
            if (Status != RentalStatus.Confirmed)
            {
                return Result.Failure(RentalErrors.NotConfirmed);
            }

            Status = RentalStatus.Completed;
            DateCompleted = utcNow;

            RaiseDomainEvent(new RentalCompletedDomainEvent(Id));
            return Result.Success();
        }

    }
}
