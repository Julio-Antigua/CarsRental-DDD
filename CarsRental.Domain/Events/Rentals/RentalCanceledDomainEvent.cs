using CarsRental.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsRental.Domain.Events.Rentals
{
    public sealed record RentalCanceledDomainEvent(Guid rentalId) : IDomainEvent;
    
}
