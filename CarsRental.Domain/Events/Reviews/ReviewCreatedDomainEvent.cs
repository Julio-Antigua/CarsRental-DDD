using CarsRental.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsRental.Domain.Events.Reviews
{
    public sealed record ReviewCreatedDomainEvent(Guid reviewId): IDomainEvent;
    
}
