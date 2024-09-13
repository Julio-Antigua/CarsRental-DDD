using CarsRental.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsRental.Domain.Exceptions
{
    public static class UserErrors
    {
        public static Error NotFound = new(
            "User.NotFound",
            "The user searched for by this id does not exist"
        );

        public static Error InvalidCredentials = new(
            "User.InvalidCredentials",
            "The credentials are incorrect"
        );
    }
}
