using CarsRental.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsRental.Domain.Exceptions
{
    public static class RentalErrors
    {
        public static Error NotFound = new Error(
            "Rental.Found",
            "The rental with the specified id was not found"
        );

        public static Error Overlap = new Error(
            "Rental.Overlap",
            "the rental is being taken by two or more clients at the same time on the same date"
        );

        public static Error NotReserved = new Error(
            "Rental.NotReserved",
            "The rental is not reserved"
        );

        public static Error NotConfirmed = new Error(
            "Rental.NotConfirmed",
            "The rental is not confirmed"
        );

        public static Error AlreadyStarted = new Error(
            "Rental.AlreadyStarted",
            "The rental has already started"
        );
    }
}
