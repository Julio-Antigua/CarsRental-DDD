using CarsRental.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsRental.Domain.ObjectsValue.Rentals
{
    public record DetailPrice(
        Currency PricePerPeriod,
        Currency MaintenancePrice,
        Currency PriceForAccessories,
        Currency TotalPrice
    );
}
