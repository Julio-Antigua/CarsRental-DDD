using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsRental.Domain.Enums
{
    public enum RentalStatus
    {
        Reserved = 1,
        Confirmed = 2,
        Decline = 3,
        Canceled = 4,
        Completed = 5
    }
}
