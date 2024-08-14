using CarsRental.Domain.Abstractions;
using CarsRental.Domain.Enums;
using CarsRental.Domain.Events.Reviews;
using CarsRental.Domain.Exceptions;
using CarsRental.Domain.ObjectsValue.Reviews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsRental.Domain.Entities
{
    public sealed class Review : Entity
    {
        private Review(
            Guid id,
            Guid carId, 
            Guid rentalId, 
            Guid userId,
            Rating rating,
            Comment comment, 
            DateTime? creationDate
        ) : base(id)
        {
            CarId = carId;
            RentalId = rentalId;
            UserId = userId;
            Rating = rating;
            Comment = comment;
            CreationDate = creationDate;
        }

        public Guid CarId { get; private set; }
        public Guid RentalId { get; private set; }
        public Guid UserId { get; private set; }

        public Rating Rating { get; private set; }
        public Comment Comment { get; private set; }
        public DateTime? CreationDate { get; private set; }

        public static Result<Review> Create(
            Rental rental,
            Rating rating,
            Comment comment,
            DateTime creationDate
        ) 
        {
            if(rental.Status != RentalStatus.Completed)
            {
                return Result.Failure<Review>(ReviewErrors.NotEligible);
            }

            var review = new Review(
                Guid.NewGuid(),
                rental.CarId,
                rental.Id,
                rental.UserId,
                rating,
                comment,
                creationDate
            );

            review.RaiseDomainEvent(new ReviewCreatedDomainEvent(review.Id));
            return review;
        }
    }
}
