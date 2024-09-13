using CarsRental.Domain.Entities;
using CarsRental.Domain.Enums;
using CarsRental.Domain.ObjectsValue.Rentals;
using CarsRental.Domain.ObjectsValue.Users;
using CarsRental.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsRental.Domain.Services.Rentals
{
    public class PriceService
    {
        public DetailPrice CalculatePrice(Vehicle vehicle, DateRange period)
        {
            var currencyType = vehicle.Price!.CurrencyType;

            var pricePerPeriod = new Currency(
                period.DayCounts * vehicle.Price.Amount,
                currencyType
                );
            decimal porcentageChange = 0;

            foreach (var accesory in vehicle.Accessories)
            {
                porcentageChange += accesory switch
                {
                    Accessories.AppleCar or Accessories.AndroidCar => 0.05m,
                    Accessories.AirConditioning => 0.01m,
                    Accessories.Maps => 0.01m,
                    _ => 0
                };
            }

            var accesoryChange = Currency.Zero(currencyType);

            if (porcentageChange > 0)
            {
                accesoryChange = new Currency(
                     pricePerPeriod.Amount * porcentageChange,
                     currencyType
                );
            }

            var totalPrice = Currency.Zero();
            totalPrice += pricePerPeriod;

            if (!vehicle!.Maintenance!.IsZero())
            {
                totalPrice += vehicle.Maintenance;
            }

            totalPrice += accesoryChange;

            return new DetailPrice(pricePerPeriod, vehicle.Maintenance, accesoryChange,totalPrice);
        }
    }
}
